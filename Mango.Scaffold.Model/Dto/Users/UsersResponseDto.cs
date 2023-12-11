using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Model.Dto.Users
{
    internal class UsersResponseDto
    {
    }

    #region 登录响应
    /// <summary>
    /// 登录响应dto
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
    }
    #endregion
}
