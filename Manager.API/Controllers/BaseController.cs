using Manager.SharedKernel;
using Manager.SharedKernel.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Manager.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase, IDisposable
    {
        protected readonly IHandler<DomainNotification> _notifications;

        public BaseController()
        {
            _notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }

        protected IActionResult ReturnResponse(object success, object error)
        {
            if (_notifications.HasNotifications())
                return BadRequest(new
                {
                    success = false,
                    data = error,
                    errors = _notifications.Notify()
                });

            return Ok(new { success = true, data = success });
        }

        public void Dispose()
        {
            _notifications.Dispose();
        }
    }
}
