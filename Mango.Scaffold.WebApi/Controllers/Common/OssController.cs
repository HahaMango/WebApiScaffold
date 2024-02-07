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
        private readonly AliyunOssOptions _options;
        private readonly OssClient _ossClient;

        public OssController(IOptions<AliyunOssOptions> options, OssClient ossClient)
        {
            _options = options.Value;
            _ossClient = ossClient;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public ApiResult<IDictionary<string, string>> Upload(IFormFile file)
        {
            //1.解析文件的key
            var timestamp = DateTime.Now.ToString("ddHHmmssfff");
            var month = DateTime.Now.ToString("yyyyMM");
            var fileName = file.FileName;
            if (fileName.Contains('.'))
            {
                //如果有扩展名，取出名称部分加上随机数
                var fileNameArray = fileName.Split('.');
                fileName = $"{fileNameArray[0]}{timestamp}.{fileNameArray[1]}";
            }
            else
            {
                //如果不存在扩展名，直接在文件名后面加时间戳
                fileName = $"{fileName}{timestamp}";
            }
            //解析文件名，增加时间戳防止重复
            var key = $"{month}/{fileName}";

            //2.上传文件
            var ossResult = _ossClient.PutObject(_options.BucketName, key, file.OpenReadStream());
            if (ossResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return Error<IDictionary<string, string>>("oss文件上传失败！");
            }
            //3.返回存储的url
            var dic = new Dictionary<string, string>
            {
                { "url", $"https://{_options.BucketName}.{_options.Endpoint}/{key}" }
            };

            return OK<IDictionary<string, string>>(dic);
        }
    }
}
