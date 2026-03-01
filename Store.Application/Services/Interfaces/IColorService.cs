using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorDto>> GetAllColorsAsync(CancellationToken cancellationToken);
        Task<ColorDto?> GetColorByIdAsync(int id, CancellationToken cancellationToken);
        Task<ColorDto> AddColorAsync(ColorDto colorDto, CancellationToken cancellationToken);
        Task<ColorDto> UpdateColorAsync(ColorDto colorDto, CancellationToken cancellationToken);
        Task DeleteColorAsync(int id, CancellationToken cancellationToken);
    }
}
