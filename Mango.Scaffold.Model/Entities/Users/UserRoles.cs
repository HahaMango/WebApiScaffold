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
    /// 用户角色数据库表实体
    /// author 小橙子
    /// date 2023/12/1
    /// </summary>
    [Table("UserRoles")]
    public class UserRoles : TimeFieldEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [Column(TypeName = "int")]
        public int RoleId { get; set; }

        /// <summary>
        /// 状态 0：失效 1：生效
        /// </summary>
        [Column(TypeName = "int")]
        public int Status { get; set; }
    }
}
