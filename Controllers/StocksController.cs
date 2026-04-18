using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project1.Dtos.Stock;
using Project1.Models;
using Project1.Repositories.Interfaces;

namespace Project1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class StocksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStockRepository _stockRepository;
    public StocksController(IMapper mapper, IStockRepository stockRepository)
    {
        _mapper = mapper;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();
        var stocksDto = stocks.Select(s => _mapper.Map<StockDto>(s));
        
        return Ok(stocksDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);
        if (stock == null)
            return NotFound();
        
        return Ok(_mapper.Map<StockDto>(stock));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequest req)
    {
        var stock = _mapper.Map<Stock>(req);
        stock = await _stockRepository.CreateAsync(stock);
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = stock.Id },
            _mapper.Map<StockDto>(stock)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateStockRequest req, int id)
    {
        var stock = await _stockRepository.UpdateAsync(id, req);
        if (stock == null)
            return NotFound();
        
        return Ok(_mapper.Map<StockDto>(stock));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var stock = await _stockRepository.DeleteAsync(id);
        if (stock == null)
            return NotFound();  
        
        return NoContent();
    }
}