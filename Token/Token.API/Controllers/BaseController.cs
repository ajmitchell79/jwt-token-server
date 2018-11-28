using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Token.BRL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Token.API.Controllers
{
    public class BaseController : Controller
    {
        private IEmailService _emailService;

        public IEmailService EmailService
        {
            get => _emailService ??
                   (_emailService = HttpContext.RequestServices.GetRequiredService<IEmailService>());
            set => _emailService = value;
        }

        public void LogError(ILogger<IController> logger, Exception ex)
        {

            var stringBuilder = new StringBuilder();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development") return;

            EmailService.SendEmail(
                "Token Error [" + env + "]",
                BuildExceptionText(stringBuilder, "<h1>Token Error</h1>",
                    User.Identity.Name + " | " + User.Identity.GetUserId(),
                    ex).ToString(),
                true
            );

            //*********************************************************
            //App Insights log - turned off for now
            //*********************************************************
            //var customData = new List<KeyValuePair<string, object>>();
            //customData.Add(new KeyValuePair<string, object>("UserName", User.Identity.Name ));
            //customData.Add(new KeyValuePair<string, object>("UserId", User.Identity.GetUserId()));

            //logger.Log(
            //    LogLevel.Error,
            //    1,
            //    customData,
            //    ex,
            //    (s, e) => e.Message); 

        }
        private StringBuilder BuildExceptionText(StringBuilder stringBuilder, string title, string user, Exception exception)
        {
            stringBuilder.Append(title).Append("<h2>").Append(exception.Message).Append("</h2><br/>")
                .Append(exception.Source ?? "").Append("<hr/>");

            stringBuilder.Append(user).Append("<hr/>");

            if (exception.StackTrace != null)
            {
                stringBuilder.Append("<h3>Stack trace: </h3><br/>").Append(exception.StackTrace.Replace(Environment.NewLine, "<br/>"));
            }

            if (exception.InnerException != null)
            {
                BuildExceptionText(stringBuilder, "<h2>Inner exception </h2>", "", exception.InnerException);
            }

            return stringBuilder;
        }
    }
}