using MediatR;

namespace Questao5.Application.Commands.MovimentacaoContaCorrente;
public class MovimentacaoContaCorrenteCommand : IRequest<MovimentarContaCorrenteCommandResult>
{
    public Guid RequisicaoId { get; set; }
    public int ContaCorrenteId { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public TipoMovimentacao Tipo { get; set; }
}
public class MovimentarContaCorrenteCommandResult
{
    public int IdMovimento { get; set; }
}

public class ValidarContaCorrenteQuery : IRequest<ValidarContaCorrenteQueryResult>
{
    public int IdContaCorrente { get; set; }

    public ValidarContaCorrenteQuery(int idContaCorrente)
    {
        IdContaCorrente = idContaCorrente;
    }
}

public class ValidarContaCorrenteQueryResult
{
    public bool Existe { get; set; }
}
