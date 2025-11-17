using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
        private readonly IUserContext _userContext;
        private readonly IValidator<OrderDto> _validator;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IUserContext userContext,
            IValidator<OrderDto> validator,
            ILogger<OrderService> logger,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
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
            var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);
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

        public async Task<OrderDto> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding New Order");
            
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
            order.OrderDate = DateTime.Now;

            var newOrder = await _orderRepository.CreateOrderAsync(order, cancellationToken);
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

            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("");
            }

            if (currentUser.Id != orderDto.UserId)
            {
                throw new Exception("Client Ids don't correspond");
            }

            var order = _mapper.Map<Order>(orderDto);

            var newOrder = await _orderRepository.UpdateOrderAsync(order, cancellationToken);
            return _mapper.Map<OrderDto>(newOrder);
        }

        public async Task<bool> DeleteOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), orderId.ToString());
            }

            await _orderRepository.DeleteOrderAsync(order, cancellationToken);
            return true;
        }

        public async Task<bool> SoftDeleteOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), orderId.ToString());
            }

            await _orderRepository.SoftDeleteOrderAsync(order, cancellationToken);
            return true;
        }
    }
}
