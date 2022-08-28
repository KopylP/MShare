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
		private readonly IContextExecutor _executor;

		public ControllerBase(IContextExecutor executor) => _executor = executor;

        protected async Task<IActionResult> ExecuteAsync<T>(IQuery<T> query) => Ok(await _executor.ExecuteAsync(query));

        protected async Task<IActionResult> ExecuteAsync<T>(ICommand<T> command) => Ok(await _executor.ExecuteAsync(command));
    }
}

