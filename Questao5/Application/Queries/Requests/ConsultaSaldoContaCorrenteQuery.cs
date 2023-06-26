using MediatR;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultaSaldoContaCorrenteQuery : IRequest<decimal>
    {
        public int ContaCorrenteId { get; set; }
    }
}
