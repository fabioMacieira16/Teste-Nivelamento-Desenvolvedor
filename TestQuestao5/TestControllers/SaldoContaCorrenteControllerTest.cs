using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.CrossCutting;
using Questao5.Infrastructure.Services.Controllers;
using Questao5.Application.Queries.Responses;

namespace TestQuestao5.TestControllers;

public class SaldoContaCorrenteControllerTest
{
    Mock<IMediator> _mockIMediator;
    LogNotifications _mockLNotifications;
    Mock<ILogger<ApiController>> _mockApiController;
    ContaCorrenteController _contaCorrenteController;

    public SaldoContaCorrenteControllerTest()
    {
        _mockIMediator = new Mock<IMediator>();
        _mockLNotifications = new LogNotifications();
        _mockApiController = new Mock<ILogger<ApiController>>();
        _contaCorrenteController = new ContaCorrenteController(_mockApiController.Object, _mockLNotifications, _mockIMediator.Object);
    }

    [Fact]
    public async void ObterSaldoContaCorrenteOk()
    {
        //Arrange
        var obterSaldoContaCorrenteQuery = new ObterSaldoContaCorrenteQuery()
        {
            Numero = 456,
        };
        var result = new ObterSaldoContaCorrenteDto();
        var statusCodeOk = (int)HttpStatusCode.OK;

        _mockIMediator.Setup(m => m.Send(It.IsAny<ObterSaldoContaCorrenteQuery>(),
                            It.IsAny<CancellationToken>()))
                            .ReturnsAsync(result)
                            .Verifiable("Notificação não foi enviada..");

        //Act
        var resultController = await _contaCorrenteController.ObterSaldoContaCorrente(obterSaldoContaCorrenteQuery);

        //Assert
        Assert.True(((IStatusCodeActionResult)resultController).StatusCode == statusCodeOk);

    }

    [Fact]
    public async void ObterSaldoContaBadRequest()
    {
        //Arrange
        var obterSaldoContaCorrenteQuery = new ObterSaldoContaCorrenteQuery()
        {
            Numero = 456,

        };
        var ret = new ObterSaldoContaCorrenteDto();
        var statusCodeBadRequest = (int)HttpStatusCode.BadRequest;
        _mockLNotifications.Add(new Notifications { Message = "Error" });

        _mockIMediator.Setup(m => m.Send(It.IsAny<ObterSaldoContaCorrenteQuery>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(ret)
                            .Verifiable("Notificação não foi enviada..");

        //Act
        var resultController = await _contaCorrenteController.ObterSaldoContaCorrente(obterSaldoContaCorrenteQuery);

        //Assert
        Assert.True(((IStatusCodeActionResult)resultController).StatusCode == statusCodeBadRequest);
    }
}
