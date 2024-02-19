using Mango.EntityFramework.BaseEntity;
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
    public class Users : TimeFieldEntity
    {
        public override void SetId()
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string? UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string? Phone { get; set; }

        /// <summary>
        /// 微信openId
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string? OpenId { get; set; }

        /// <summary>
        /// 用户密码的哈希值
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string? PasswordHash { get; set; }

        /// <summary>
        /// 状态 0：失效 1：有效
        /// </summary>
        [Column(TypeName = "int")]
        public int Status { get; set; }
    }
}
