namespace buildingLink.Repositories
{
    using buildingLink.Models;
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task<Driver> GetDriverByIdAsync(int id);
        Task<Driver> AddDriverAsync(Driver person);
        Task UpdateDriverAsync(Driver person);
        Task DeleteDriverAsync(int id);
    }
}
