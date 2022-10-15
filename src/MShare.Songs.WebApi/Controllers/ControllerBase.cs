using System;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.Api;
using MShare.Framework.WebApi.Filters;

namespace MShare.Songs.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiExceptionFilter]
    [ApiController]
    public class ControllerBase : Controller
	{
		private readonly IHttpContextExecutor _executor;

		public ControllerBase(IHttpContextExecutor executor) => _executor = executor;

        protected async Task<IActionResult> ExecuteAsync<T>(IQuery<T> query) => Ok(await _executor.ExecuteAsync(query));

        protected async Task<IActionResult> ExecuteAsync(ICommand command) => Ok(await _executor.ExecuteAsync(command));
    }
}

