using Desafio_B3.Model;
using Microsoft.EntityFrameworkCore;

namespace Desafio_B3.data;

public class LiveOrderBookContext : DbContext
{

    public LiveOrderBookContext(DbContextOptions<LiveOrderBookContext> options) : base(options)
    {
        
    }

    public DbSet<Bitstamp> Bitstamps { get; set; }

}
