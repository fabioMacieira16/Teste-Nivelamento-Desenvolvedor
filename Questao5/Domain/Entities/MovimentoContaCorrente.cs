using Questao5.Application.Commands.MovimentacaoContaCorrente;

namespace Questao5.Domain.Entities;
public class MovimentoContaCorrente
{
    public Guid Id { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public DateTime DataHora { get; set; }


    public MovimentoContaCorrente(TipoMovimentacao tipo, decimal valor, string descricao)
    {
        Id = Guid.NewGuid();
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;
        DataHora = DateTime.UtcNow;
    }
}
