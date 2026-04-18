using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Dtos.Stock;
using Project1.Models;
using Project1.Repositories.Interfaces;

namespace Project1.Repositories.Implementations;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    
    public StockRepository(ApplicationDBContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(stock => stock.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (stock == null)
        {
            return null;
        }
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _context.Stocks.AnyAsync(stock => stock.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest req)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (stock == null)
            return null;

        _mapper.Map(req, stock);
        await _context.SaveChangesAsync();
        return stock;
    }
}