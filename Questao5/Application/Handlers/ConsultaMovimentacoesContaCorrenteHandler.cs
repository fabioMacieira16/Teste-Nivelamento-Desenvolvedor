using MediatR;
using Questao5.Application.Commands.MovimentacaoContaCorrente;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Exceptions;
using Questao5.Infrastructure.Repository;

namespace Questao5.Application.Handlers;

public class ConsultaMovimentacoesContaCorrenteHandler : 
    IRequestHandler<ConsultaMovimentacoesContaCorrenteQuery, List<MovimentoContaCorrente>>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public ConsultaMovimentacoesContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
    }

    public async Task<List<MovimentoContaCorrente>> Handle(ConsultaMovimentacoesContaCorrenteQuery request, CancellationToken cancellationToken)
    {
        var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteByIdAsync(request.ContaCorrenteId);

        if (contaCorrente == null)
        {
            throw new ContaCorrenteNotFoundException(request.ContaCorrenteId);
        }


        return contaCorrente.Movimentacoes;
    }

    //public async Task<decimal> Handle(ConsultaSaldoContaCorrenteQuery query, CancellationToken cancellationToken)
    //{
    //    // Obtém a conta corrente correspondente à consulta
    //    var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteByIdAsync(query.ContaCorrenteId);

    //    // Verifica se a conta corrente existe
    //    if (contaCorrente == null)
    //    {
    //        throw new ContaCorrenteNotFoundException(query.ContaCorrenteId);
    //    }

    //    // Obtém a lista de movimentos da conta corrente
    //    var movimentos = await _movimentoRepository.ObterMovimentosAsync(query.ContaCorrenteId);

    //    // Calcula o saldo atual da conta corrente
    //    var saldoAtual = movimentos.Sum(m => m.TipoMovimento == TipoMovimentacao.Credito ? m.Valor : -m.Valor);

    //    return saldoAtual;
    //}
}