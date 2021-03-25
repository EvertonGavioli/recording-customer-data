using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RCD.Core.Notifications;
using System.Linq;

namespace RCD.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected ActionResult ApiResponse(object result = null)
        {
            if (_notifier.HasNotification())
            {
                return BadRequest(new
                {
                    success = false,
                    errors = _notifier.GetNotifications().Select(p => p.Message)
                });
            } 
            else
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
        }

        protected ActionResult ApiResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorModelState(modelState);
            return ApiResponse();
        }

        protected void NotifyErrorModelState(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
