using MediatR;
using Questao5.Infrastructure.Exceptions;
using Questao5.Infrastructure.Repository;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.MovimentacaoContaCorrente;
public class MovimentacaoContaCorrenteCommandHandler : IRequestHandler<MovimentacaoContaCorrenteCommand>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMovimentoRepository _movimentoRepository;

    public MovimentacaoContaCorrenteCommandHandler(IContaCorrenteRepository contaCorrenteRepository, IMovimentoRepository movimentoRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _movimentoRepository = movimentoRepository;
    }

    public async Task<Unit> Handle(MovimentacaoContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        // Verificar se a conta corrente existe
        var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteByIdAsync(command.ContaCorrenteId);

        if (contaCorrente == null)
        {
            throw new ContaCorrenteNotFoundException(command.ContaCorrenteId);
        }

        // Verificar se o tipo de movimentação é válido
        if (command.Tipo != TipoMovimentacao.Credito && command.Tipo != TipoMovimentacao.Debito)
        {
            throw new TipoMovimentacaoInvalidoException();
        }

        // Registrar a movimentação na conta corrente
        var movimento = new MovimentoContaCorrente(command.Tipo, command.Valor, command.Descricao);
        contaCorrente.AdicionarMovimento(movimento);

        // Retornar a instância de Unit
        return Unit.Value;
    }
}