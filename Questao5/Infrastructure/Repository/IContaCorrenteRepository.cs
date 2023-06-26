using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repository
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetContaCorrenteByIdAsync(int id);
    }
}
