using Questao5.Application.Queries.Responses;
using MediatR;

namespace Questao5.Application.Queries.Requests;

public class ObterSaldoContaCorrenteQuery : IRequest<ObterSaldoContaCorrenteDto>
{
    public int Numero { get; set; }
}
