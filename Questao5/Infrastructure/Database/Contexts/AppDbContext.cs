
using Microsoft.EntityFrameworkCore;
using Questao5.Infrastructure.Database.Mapping;

namespace Questao5.Infrastructure.Database.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContaCorrenteMapping());
        modelBuilder.ApplyConfiguration(new MovimentoMapping());
        modelBuilder.ApplyConfiguration(new IdEmpotenciaMapping());
    }
}