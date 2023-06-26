using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests;

public class ConsultaContaCorrentePorIdQuery : IRequest<ContaCorrente>
{
    public int ContaCorrenteId { get; set; }
}
