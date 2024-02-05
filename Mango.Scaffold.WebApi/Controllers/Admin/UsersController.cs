using Mango.Core.ApiResponse;
using Mango.Core.ControllerAbstractions;
using Mango.Scaffold.Model.Dto.Users;
using Mango.Scaffold.Model.Entities.Users;
using Mango.Scaffold.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mango.Scaffold.WebApi.Controllers.Admin
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class UsersController : MangoUserApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("addUsers")]
        public async Task<ApiResult> AddUsers(AddUsersRequestDto dto)
        {
            ThrowIfModelsInValid();
            return await _userService.AddUsersAsync(dto);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ApiResult<LoginResponseDto>> Login(LoginRequestDto dto)
        {
            ThrowIfModelsInValid();
            return await _userService.LoginAsync(dto);
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("userInfo")]
        public async Task<ApiResult<Users>> QueryUserInfo()
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return await _userService.QueryUserInfoAsync(Convert.ToInt32(id.Value));
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public async Task<ApiResult> Test()
        {
            throw new Mango.Core.Exceptions.ServiceException("测试异常");
        }
    }
}
