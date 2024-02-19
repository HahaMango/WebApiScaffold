using Mango.Core.ApiResponse;
using Mango.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Scaffold.WebApi.Controllers
{
    /// <summary>
    /// 全局异常处理页
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <returns></returns>
        [Route("/api/error")]
        public Task<ApiResult<object>> ErrorHandler()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            HttpContext.Response.StatusCode = 200;

            var exception = exceptionHandlerPathFeature?.Error;
            if (exception != null)
            {
                var isAppException = exception is ServiceException;

                return Task.FromResult(new ApiResult<object>
                {
                    Code = isAppException ? (exception as ServiceException).Code : Core.Enums.Code.InternalServerError,
                    Message = exception.Message,
                    Data = isAppException ? (exception as ServiceException).E : null
                });
            }
            return Task.FromResult(new ApiResult<object>
            {
                Code = Core.Enums.Code.InternalServerError
            });
        }
    }
}
