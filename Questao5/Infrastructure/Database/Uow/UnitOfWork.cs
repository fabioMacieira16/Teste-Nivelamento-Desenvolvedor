using Questao5.Domain.Repository;
using Questao5.Infrastructure.Database.Contexts;

namespace Questao5.Infrastructure.Database.Uow;

public class UnitOfWork : IUnitOfWork
{

    readonly AppDbContext _aplicationContext;
    public UnitOfWork(AppDbContext aplicationContext)
    {
        _aplicationContext = aplicationContext;
    }
    public void Dispose() => GC.SuppressFinalize(this);
    public async Task<bool> CommitAsync() => await _aplicationContext.SaveChangesAsync() > 0;
}
