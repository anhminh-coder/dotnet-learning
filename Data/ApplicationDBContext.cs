using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Data;

public class ApplicationDBContext: DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
    {
        
    }
    
    public DbSet<Stock>  Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}