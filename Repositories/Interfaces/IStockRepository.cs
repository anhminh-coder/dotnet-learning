using Project1.Dtos.Stock;
using Project1.Models;

namespace Project1.Repositories.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stock);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequest req);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> IsExistAsync(int id);
}