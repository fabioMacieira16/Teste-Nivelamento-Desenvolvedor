using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ContaCorrenteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContaCorrenteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<MovimentaContaCorrenteCommandResult>> MovimentarContaCorrente(MovimentarContaCorrenteCommand command)
    {
        // Executa uma consulta para validar a existência da conta corrente
        var validarContaCorrenteQuery = new ValidarContaCorrenteQuery(command.IdContaCorrente);
        var validarContaCorrenteQueryResult = await _mediator.Send(validarContaCorrenteQuery);

        // Verifica se a conta corrente existe
        if (!validarContaCorrenteQueryResult.Existe)
        {
            return BadRequest("Conta corrente inválida");
        }

        // Executa o comando para movimentar a conta corrente
        var movimentarContaCorrenteCommandResult = await _mediator.Send(command);

        return Ok(movimentarContaCorrenteCommandResult);
    }

    [HttpGet("{contaCorrenteId}")]
    public async Task<ActionResult<ContaCorrente>> ConsultarContaCorrentePorId(int contaCorrenteId)
    {
        var consultaContaCorrentePorIdQuery = new ConsultaContaCorrentePorIdQuery
        {
            ContaCorrenteId = contaCorrenteId
        };

        var contaCorrente = await _mediator.Send(consultaContaCorrentePorIdQuery);

        if (contaCorrente == null)
        {
            return NotFound();
        }

        return Ok(contaCorrente);
    }

    [HttpGet("{contaCorrenteId}/movimentacoes")]
    public async Task<ActionResult<List<MovimentoContaCorrente>>> ConsultarMovimentacoesContaCorrente(int contaCorrenteId)
    {
        var consultaMovimentacoesContaCorrenteQuery = new
            ConsultaMovimentacoesContaCorrenteQuery
        {
            ContaCorrenteId = contaCorrenteId
        };
        var movimentacoes = await _mediator.Send(consultaMovimentacoesContaCorrenteQuery);

        if (movimentacoes == null)
        {
            return NotFound();
        }

        return Ok(movimentacoes);
    }
}