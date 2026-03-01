using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface IColorRepository
    {
        Task<List<Color>> GetAllColorsAsync(CancellationToken cancellationToken);
        Task<Color?> GetColorByIdAsync(int id, CancellationToken cancellationToken);
        Task<Color> AddColorAsync(Color color, CancellationToken cancellationToken);
        Task<Color> UpdateColorAsync(Color color, CancellationToken cancellationToken);
        Task DeleteColorAsync(Color color, CancellationToken cancellationToken);
    }
}
