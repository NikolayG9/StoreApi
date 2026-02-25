using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Application.Common.Constants;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Application.User.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Repositories;

namespace Store.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMailService _mailService;
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IUserContext _userContext;
        private readonly IValidator<OrderDto> _validator;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IMailService mailService,
            IPdfGeneratorService pdfGeneratorService,
            IUserContext userContext,
            IValidator<OrderDto> validator,
            ILogger<OrderService> logger,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mailService = mailService;
            _pdfGeneratorService = pdfGeneratorService;
            _userContext = userContext;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting All Orders");
            var orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Order Details By Id - {orderId}");
            var user = _userContext.GetCurrentUser();
            var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);

            if (user == null || user.Id != order.UserId)
            {
                return null;
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByClientId(CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("");
            }
            _logger.LogInformation($"Getting Orders By Client Id - {currentUser.Id}");

            var order = await _orderRepository.GetOrdersByClientIdAsync(currentUser.Id, cancellationToken);
            return _mapper.Map<IEnumerable<OrderDto>>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllSoftDeletedOrdersAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting All Soft Deleted Orders");

            var softDeletedOrders = await _orderRepository.GetAllSoftDeletedOrdersAsync(cancellationToken);
            return _mapper.Map<IEnumerable<OrderDto>>(softDeletedOrders);
        }

        public async Task<OrderDto> GetSoftDeletedOrderInformationByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting Soft Deleted Order Information");

            var softDeletedOrder = await _orderRepository.GetSoftDeletedOrderInformationByIdAsync(orderId, cancellationToken);
            return _mapper.Map<OrderDto>(softDeletedOrder);
        }

        public async Task<byte[]?> GetOrderPdfFileAsync(int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Order PDF File By Id - {orderId}");
            var user = _userContext.GetCurrentUser();
            var orderDetails = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);
            
            if (user == null || user.Id != orderDetails.UserId)
            {
                return null;
            }

            var orderPdf = await _pdfGeneratorService.GenerateOrderPdfFileAsync(orderId, cancellationToken);
            return orderPdf;
        }

        public async Task<OrderDto> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding New Order");
            orderDto.Status = OrderStatus.New;

            var validationResult = await _validator.ValidateAsync(orderDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var allErrors = string.Join("; ", validationResult.Errors
                      .Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                throw new NotValidDtoException(nameof(OrderDto), allErrors);
            }

            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null) 
            {
                throw new UnauthorizedAccessException("");
            }

            var order = _mapper.Map<Order>(orderDto);
            order.UserId = currentUser.Id;
            order.OrderDate = DateTime.UtcNow;

            var newOrder = await _orderRepository.CreateOrderAsync(order, cancellationToken);

                var pdfData = await _pdfGeneratorService.GenerateOrderPdfFileAsync(newOrder.Id, cancellationToken);

            var emailSubject = $"Order Confirmation – Elegant Bride Boutique – Order #{newOrder.Id}";
            var emailBody = EmailMessageConstants.OrderMessageBody
               .Replace("[Customer_Name]", $"{newOrder?.OrderInformation?.FirstName} {newOrder?.OrderInformation?.LastName}")
               .Replace("[Order_Number]", newOrder?.Id.ToString());

            await _mailService.SendEmailAsync(newOrder.OrderInformation.Email, emailSubject, emailBody, pdfData, cancellationToken);
           
            return _mapper.Map<OrderDto>(newOrder);
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderDto orderDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating Order {orderDto.Id}");

            var validationResult = await _validator.ValidateAsync(orderDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var allErrors = string.Join("; ", validationResult.Errors
                      .Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                throw new NotValidDtoException(nameof(OrderDto), allErrors);
            }

            if (await _orderRepository.IsAnyOrderByIdAsync(orderDto.Id, cancellationToken))
            {
                throw new NotFoundException(nameof(Order), orderDto.Id.ToString());
            }

            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("User is unauthorized");
            }

            var order = _mapper.Map<Order>(orderDto);

            var newOrder = await _orderRepository.UpdateOrderAsync(order, cancellationToken);
            return _mapper.Map<OrderDto>(newOrder);
        }

        public async Task<OrderDto> CancelSoftDeletedOrderById(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetSoftDeletedOrderInformationByIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), orderId.ToString());
            }

            order.IsSoftDeleted = true;
            await _orderRepository.UpdateOrderAsync(order, cancellationToken);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), orderId.ToString());
            }

            await _orderRepository.DeleteOrderAsync(order, cancellationToken);
        }

        public async Task SoftDeleteOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), orderId.ToString());
            }

            await _orderRepository.SoftDeleteOrderAsync(order, cancellationToken);
        }
    }
}
