using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Model.Dto.Users
{
    internal class UsersRequestDto
    {
    }

    #region 添加用户dto
    /// <summary>
    /// 添加用户Dto
    /// </summary>
    public class AddUsersRequestDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 密码哈希值
        /// </summary>
        public string Password { get; set; }
    }
    #endregion

    #region 登录dto
    /// <summary>
    /// 登录dto
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
    #endregion
}
