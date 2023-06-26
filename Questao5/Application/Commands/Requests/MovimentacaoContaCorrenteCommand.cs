using Questao5.Application.Commands.Responses;
using MediatR;

namespace Questao5.Application.Commands.Requests;
public class MovimentacaoContaCorrenteCommand : IRequest<MovimentarContaCorrenteResponse>
{
    public string TipoMovimento { get; set; } = "";

    public int Numero { get; set; }

    public decimal Valor { get; set; }


    public MovimentacaoContaCorrenteCommand() { }
}
