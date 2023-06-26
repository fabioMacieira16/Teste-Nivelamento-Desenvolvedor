using Questao5.Application.Commands.MovimentacaoContaCorrente;

namespace Questao5.Domain.Entities;
public class ContaCorrente
{
    public int Numero { get; set; }
    public string Nome { get; set; }
    public decimal Saldo { get; set; }
    public DateTime DataMovimento { get; set; }

    public List<MovimentoContaCorrente> Movimentacoes { get; set; }

    public ContaCorrente()
    {
        Movimentacoes = new List<MovimentoContaCorrente>();
    }

    public void AdicionarMovimento(MovimentoContaCorrente movimentacao)
    {
        Movimentacoes.Add(movimentacao);

        if (movimentacao.Tipo == TipoMovimentacao.Credito)
        {
            Saldo += movimentacao.Valor;
        }
        else if (movimentacao.Tipo == TipoMovimentacao.Debito)
        {
            Saldo -= movimentacao.Valor;
        }
    }


}
