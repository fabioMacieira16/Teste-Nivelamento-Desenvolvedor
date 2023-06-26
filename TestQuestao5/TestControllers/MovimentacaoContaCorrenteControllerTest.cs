using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.CrossCutting;
using Questao5.Infrastructure.Services.Controllers;

namespace TestQuestao5.TestControllers;

public class MovimentacaoContaCorrenteControllerTest
{
    Mock<IMediator> _mockIMediator;
    LogNotifications _mockLNotifications;
    Mock<ILogger<ApiController>> _mockApiController;
    ContaCorrenteController _contaCorrenteController;

    public MovimentacaoContaCorrenteControllerTest()
    {
        _mockIMediator = new Mock<IMediator>();
        _mockLNotifications = new LogNotifications();
        _mockApiController = new Mock<ILogger<ApiController>>();
        _contaCorrenteController = new ContaCorrenteController(_mockApiController.Object, _mockLNotifications, _mockIMediator.Object);

    }

    [Fact]
    public async void MovimentacaoContaCorrenteOk()
    {
        //Arrange
        // monta a entrada dos dados  
        var movimentacaoContaCorrenteCommand = new MovimentacaoContaCorrenteCommand()
        {
            Numero = 100,
            TipoMovimento = "C",
            Valor = 999
        };

        var result = new MovimentarContaCorrenteResponse();

        var statusCodeOk = (int)HttpStatusCode.OK;
        _mockIMediator.Setup(movimenta => movimenta.Send(It.IsAny<MovimentacaoContaCorrenteCommand>(),
                                                It.IsAny<CancellationToken>()))
                            .ReturnsAsync(result)
                            .Verifiable("Notificação não foi enviada...");

        //Act
        var resultController = await 
            _contaCorrenteController.MovimentacaoContaCorrente(movimentacaoContaCorrenteCommand);

        //Assert
        Assert.True(((IStatusCodeActionResult)resultController).StatusCode == statusCodeOk);

    }

    [Fact]
    public async void MovimentacaoContaCorrenteBadRequest()
    {
        //Arrange
        //passando dados errados
        var movimentacaoContaCorrenteCommand = new MovimentacaoContaCorrenteCommand()
        {
            Numero = 10,
            TipoMovimento = "d",
            Valor = 99
        };
        var result = new MovimentarContaCorrenteResponse();

        var statusCodeBadRequest = (int)HttpStatusCode.BadRequest;
        _mockLNotifications.Add(new Notifications { Message = "Error" });

        _mockIMediator.Setup(m => m.Send(It.IsAny<MovimentacaoContaCorrenteCommand>(), 
                            It.IsAny<CancellationToken>()))
                            .ReturnsAsync(result)
                            .Verifiable("Notificação não foi enviada...");

        //Act
        var resultController = await 
            _contaCorrenteController.MovimentacaoContaCorrente(movimentacaoContaCorrenteCommand);

        //Assert
        Assert.True(((IStatusCodeActionResult)resultController).StatusCode == statusCodeBadRequest);
    }
}
