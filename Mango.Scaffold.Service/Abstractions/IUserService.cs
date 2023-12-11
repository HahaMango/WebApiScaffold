using Mango.Core.ApiResponse;
using Mango.Scaffold.Model.Dto.Users;
using Mango.Scaffold.Model.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Service.Abstractions
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> AddUsersAsync(AddUsersRequestDto dto);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult<LoginResponseDto>> LoginAsync(LoginRequestDto dto);

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApiResult<Users>> QueryUserInfoAsync(int userId);
    }
}
