using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests;

public class ConsultaMovimentacoesContaCorrenteQuery : IRequest<List<MovimentoContaCorrente>>
{
    public int ContaCorrenteId { get; set; }
}
