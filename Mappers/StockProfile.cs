using AutoMapper;
using Project1.Dtos.Stock;
using Project1.Models;

namespace Project1.Mappers;

public class StockProfile : Profile
{
    public StockProfile()
    {
        // Create
        CreateMap<CreateStockRequest, Stock>();

        // Update
        CreateMap<UpdateStockRequest, Stock>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // Read
        CreateMap<Stock, StockDto>();
    }
}