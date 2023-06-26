using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Infrastructure.CrossCutting;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers;

//[ApiController]
[Route("api/contacorrente")]
public class ContaCorrenteController : ApiController
{

    IMediator _mediator;

    public ContaCorrenteController(ILogger<ApiController> logger, LogNotifications notifications,
                                         IMediator mediator) : base(logger, notifications)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// MovimentacaoContaCorrenteCommand => Movimentar a conta com créditos ou débitos
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna o item solicitado</response>
    /// <response code="400">Regras de negócios inválidas ou solicitação mal formatada</response>   
    /// <response code="500">Erro do Servidor Interno</response>   
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> MovimentacaoContaCorrente([FromBody] MovimentacaoContaCorrenteCommand movimentacaoContaCorrenteCommand)
        => await ExecControllerAsync(() => _mediator.Send(movimentacaoContaCorrenteCommand));


    /// <summary>
    /// Obter o Saldo da Conta Corrente
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna o item solicitado</response>
    /// <response code="400">Regra inválidas ou solicitação mal formatada</response>   
    /// <response code="500">Erro do Servidor Interno</response>   
    /// <response code="401">Não autorizado</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> ObterSaldoContaCorrente([FromQuery] ObterSaldoContaCorrenteQuery obterSaldoContaCorrenteQuery)
        => await ExecControllerAsync(() => _mediator.Send(obterSaldoContaCorrenteQuery));
}