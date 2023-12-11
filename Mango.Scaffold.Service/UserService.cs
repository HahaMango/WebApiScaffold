using Mango.Core.ApiResponse;
using Mango.Core.Authentication.Jwt;
using Mango.Core.Encryption;
using Mango.Core.Extension;
using Mango.EntityFramework.Abstractions;
using Mango.Scaffold.Model.Dto.Users;
using Mango.Scaffold.Model.Entities.Users;
using Mango.Scaffold.Repository.Abstractions;
using Mango.Scaffold.Service.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Service
{
    /// <summary>
    /// 用户服务接口实现
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IUserRolesRepository _userRolesRepository;

        private readonly MangoJwtTokenHandler _mangoJwtTokenHandler;

        public UserService(
            ILogger<UserService> logger,
            IUsersRepository usersRepository,
            IRolesRepository rolesRepository,
            IUserRolesRepository userRolesRepository,
            MangoJwtTokenHandler mangoJwtTokenHandler)
        {
            _logger = logger;

            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _userRolesRepository = userRolesRepository;

            _mangoJwtTokenHandler = mangoJwtTokenHandler;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddUsersAsync(AddUsersRequestDto dto)
        {
            var response = new ApiResult();
            try
            {
                ArgumentNullException.ThrowIfNull(dto);
                //根据名称去重
                if (await _usersRepository.TableNotTracking.AnyAsync(x => x.UserName == dto.UserName && x.Status == 1))
                {
                    response.Code = Core.Enums.Code.Error;
                    response.Message = "用户名已存在！";
                    return response;
                }

                var users = dto.MapTo<AddUsersRequestDto, Users>();
                users.PasswordHash = !string.IsNullOrWhiteSpace(dto.Password) ? MangoSHA256.Encrypt2String(dto.Password) : null;
                users.Status = 1;
                users.CreateTime = DateTime.Now;
                users.CreateBy = "init";

                await _usersRepository.InsertAsync(users);
                await _usersRepository.UnitOfWork.SaveChangesAsync();

                response.Code = Core.Enums.Code.Ok;
                response.Message = "添加成功";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"添加用户异常：{ex}");

                response.Code = Core.Enums.Code.Error;
                response.Message = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult<LoginResponseDto>> LoginAsync(LoginRequestDto dto)
        {
            var response = new ApiResult<LoginResponseDto>();
            try
            {
                ArgumentNullException.ThrowIfNull(dto);
                //1.获取用户
                var user = await _usersRepository.TableNotTracking.FirstOrDefaultAsync(x => x.UserName == dto.UserName && x.Status == 1);
                if (user == null)
                {
                    response.Code = Core.Enums.Code.Error;
                    response.Message = "查无用户！";
                    return response;
                }
                //2.校验密码
                if (user.PasswordHash != MangoSHA256.Encrypt2String(dto.Password))
                {
                    response.Code = Core.Enums.Code.Error;
                    response.Message = "密码错误！";
                    return response;
                }
                //3.获取角色
                var roles = await (from ur in _userRolesRepository.TableNotTracking
                                   join r in _rolesRepository.TableNotTracking on ur.RoleId equals r.Id
                                   where ur.Status == 1 && r.Status == 1 && ur.UserId == user.Id
                                   select r.RoleType).ToListAsync();
                //4.生成claim
                var claims = new List<Claim>
                {
                    //userName
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                //5.生成token
                var token = await _mangoJwtTokenHandler.IssuedTokenAsync(claims.ToArray());
                response.Code = Core.Enums.Code.Ok;
                response.Message = "登录成功";
                response.Data = new LoginResponseDto
                {
                    Token = token
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"登录异常：{ex}");

                response.Code = Core.Enums.Code.Error;
                response.Message = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResult<Users>> QueryUserInfoAsync(int userId)
        {
            var response = new ApiResult<Users>();
            try
            {
                var users = await _usersRepository.TableNotTracking.FirstOrDefaultAsync(x => x.Id == userId && x.Status == 1);
                response.Code = Core.Enums.Code.Ok;
                response.Message = "查询成功";
                response.Data = users;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"查询用户信息异常：{ex}");

                response.Code = Core.Enums.Code.Error;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
