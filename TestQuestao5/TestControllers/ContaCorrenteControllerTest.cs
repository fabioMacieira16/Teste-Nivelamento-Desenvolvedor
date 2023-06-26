using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Questao5.Infrastructure.CrossCutting;
using Questao5.Infrastructure.Services.Controllers;

namespace TestQuestao5.TestControllers;

public class ContaCorrenteControllerTest
{
    Mock<IMediator> _mockIMediator;
    LogNotifications _mockLNotifications;
    Mock<ILogger<ApiController>> _mockApiController;

}
