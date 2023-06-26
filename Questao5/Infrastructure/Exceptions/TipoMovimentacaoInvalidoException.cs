using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Exceptions
{
    public class TipoMovimentacaoInvalidoException : IOException
    {
        public TipoMovimentacaoInvalidoException()
             : base($"tipo de movimentação não é válido")
        {}
    }
}
