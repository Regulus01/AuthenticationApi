using System.Net;
using Infra.CrossCutting.Util.Configuration.Core.Response;
using Infra.CrossCutting.Util.Notifications.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Infra.CrossCutting.Util.Configuration.Core.Controllers;

public abstract class CoreController : ControllerBase
{
    private readonly INotify _notifications;

    protected CoreController(INotify notification)
    {
        _notifications = notification;
    }

    protected new IActionResult Response(object? result = null)
    {
        if (!HasNotifications())
        {
            if (result != null)
                return Ok(new ReponseModel
                {
                    Success = true,
                    Data = result
                });

            return Ok(new ReponseModel
            {
                Success = true
            });
        }

        return NoticationsEntity();
    }

    private bool HasNotifications()
    {
        return _notifications.HasNotifications();
    }

    private IActionResult NoticationsEntity()
    {
        var notifications = _notifications.GetNotifications();

        return BadRequest(new ApiResponse(HttpStatusCode.BadRequest.ToString(), notifications.ToList()));
    }
}