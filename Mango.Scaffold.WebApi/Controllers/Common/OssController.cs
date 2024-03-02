using Aliyun.OSS;
using Mango.Core.Aliyun.OSS;
using Mango.Core.ApiResponse;
using Mango.Core.ControllerAbstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mango.Scaffold.WebApi.Controllers.Common
{
    /// <summary>
    /// oss相关控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OssController : MangoUserApiController
    {
        private readonly AliyunOssApi _aliyunOssApi;

        public OssController(AliyunOssApi aliyunOssApi)
        {
            _aliyunOssApi = aliyunOssApi;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<ApiResult<IDictionary<string, string>>> Upload(IFormFile file)
        {
            var fileName = file.FileName;
            //上传文件
            var ossResult = await _aliyunOssApi.UploadFileAsync(fileName, file.OpenReadStream());
            //返回存储的url
            var dic = new Dictionary<string, string>
            {
                { "url", ossResult }
            };

            return OK<IDictionary<string, string>>(dic);
        }
    }
}
