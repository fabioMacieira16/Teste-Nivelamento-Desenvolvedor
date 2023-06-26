
using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<ContaCorrente> ContasCorrentes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContaCorrente>();

    }
}