using MediatR;
using Questao5.Application.Commands.MovimentacaoContaCorrente;
using Questao5.Infrastructure.Repository;

namespace Questao5.Application.Handlers;

public class MovimentarContaCorrenteCommandHandler : IRequestHandler<MovimentacaoContaCorrenteCommand, MovimentarContaCorrenteCommandResult>
{
    private readonly IMediator _mediator;
    private readonly IMovimentoRepository _movimentoRepository;

    public MovimentarContaCorrenteCommandHandler(IMediator mediator, IMovimentoRepository movimentoRepository)
    {
        _mediator = mediator;
        _movimentoRepository = movimentoRepository;
    }

    public async Task<MovimentarContaCorrenteCommandResult> Handle(MovimentacaoContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        // Executa a movimentação da conta corrente
        var movimento = new Movimento(command.ContaCorrenteId, command.Valor, command.Tipo);
        await _movimentoRepository.AddAsync(movimento);

        return new MovimentarContaCorrenteCommandResult { IdMovimento = movimento.Id };
    }
}

public class ValidarContaCorrenteQueryHandler : IRequestHandler<ValidarContaCorrenteQuery, ValidarContaCorrenteQueryResult>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public ValidarContaCorrenteQueryHandler(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
    }

    public async Task<ValidarContaCorrenteQueryResult> Handle(ValidarContaCorrenteQuery query, CancellationToken cancellationToken)
    {
        // Verifica se a conta corrente existe
        var contaCorrente = await _contaCorrenteRepository.GetContaCorrenteByIdAsync(query.IdContaCorrente);

        return new ValidarContaCorrenteQueryResult { Existe = contaCorrente != null };
    }
}