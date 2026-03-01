using AutoMapper;
using Microsoft.Extensions.Logging;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Application.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ColorService> _logger;

        public ColorService(
            IColorRepository colorRepository,
            IMapper mapper,
            ILogger<ColorService> logger)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ColorDto>> GetAllColorsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting All Colors");
            var colors = await _colorRepository.GetAllColorsAsync(cancellationToken);
            return _mapper.Map<List<ColorDto>>(colors);
        }

        public async Task<ColorDto?> GetColorByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Color By Id - {id}");
            var color = await _colorRepository.GetColorByIdAsync(id, cancellationToken);
            return _mapper.Map<ColorDto>(color);
        }
        
        public async Task<ColorDto> AddColorAsync(ColorDto colorDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Add New Color");
            if (colorDto == null)
            {
                throw new ArgumentNullException("Color was null");
            }

            var color = _mapper.Map<Color>(colorDto);
            await _colorRepository.AddColorAsync(color, cancellationToken);

            return _mapper.Map<ColorDto>(color);
        }
        
        public async Task<ColorDto> UpdateColorAsync(ColorDto colorDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update existing color");
            if (colorDto == null)
            {
                throw new ArgumentNullException("Color was null");
            }

            var color = _mapper.Map<Color>(colorDto);
            await _colorRepository.UpdateColorAsync(color, cancellationToken);

            return _mapper.Map<ColorDto>(color);
        }

        public async Task DeleteColorAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Delete Color By Id - {id}");
            var color = await _colorRepository.GetColorByIdAsync(id, cancellationToken);
            if (color == null)
            {
                throw new ArgumentNullException("Color not exists with such id");
            }

            await _colorRepository.DeleteColorAsync(color, cancellationToken);
        }
    }
}
