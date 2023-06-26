using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repository
{
    public interface IMovimentoRepository
    {
        Task<MovimentoContaCorrente> ObterPorIdAsync(Guid id);
    }
}
