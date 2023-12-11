using Mango.EntityFramework.BaseEntity;
using Mango.Scaffold.Model.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Model.Entities.Users
{
    /// <summary>
    /// 用户表实体
    /// author 小橙子
    /// date 2023/12/1
    /// </summary>
    [Table("Users")]
    public class Users : BaseFieldEntity
    {
        public override void SetId()
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 微信openId
        /// </summary>
        public string? OpenId { get; set; }

        /// <summary>
        /// 用户密码的哈希值
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// 状态 0：失效 1：有效
        /// </summary>
        public int Status { get; set; }
    }
}
