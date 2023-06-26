using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questao5.Infrastructure.CrossCutting;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly LogNotifications _notifications;
    protected readonly ILogger _logger;

    public ApiController(ILogger<ApiController> logger,
                          LogNotifications notifications)
    {
        _notifications = notifications;
        _logger = logger;
    }

    [NonAction]
    public bool IsValid()
    {
        return !_notifications.Any();
    }

    [NonAction]
    void LoggerException(Exception ex)
    {
        var err = new ErrorLog();
        err.StackTrace = ex.StackTrace;
        err.Message = ex.Message;
        err.InnerException = ex.InnerException?.ToString();
        _logger.LogError(JsonConvert.SerializeObject(err));
    }

    [NonAction]
    protected async Task<IActionResult> ExecControllerAsync<T>
                        (Func<Task<T>> func)
    {
        try
        {
            return Response(await func());
        }
        catch (Exception ex)
        {
            LoggerException(ex);
            AddError(ex);
            return Response(null);
        }
    }

    protected new IActionResult Response(object result = null)
    {
        if (IsValid())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            data = result,
            errors = _notifications
        });
    }


    [NonAction]
    protected void AddError(Exception except)
    {
        _notifications.Add(new Notifications { Message = except.Message });
    }

    [NonAction]

    protected void AddError(Notifications erro)
    {
        _notifications.Add(erro);
    }
}
