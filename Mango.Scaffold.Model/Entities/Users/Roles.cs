﻿using Mango.EntityFramework.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Model.Entities.Users
{
    /// <summary>
    /// 角色数据库表实体
    /// author 小橙子
    /// date 2023/12/1
    /// </summary>
    [Table("Roles")]
    public class Roles : TimeFieldEntity
    {
        /// <summary>
        /// 角色类型
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string RoleType { get; set; }

        /// <summary>
        /// 是否生效 0：失效 1：生效
        /// </summary>
        [Column(TypeName = "int")]
        public int Status { get; set; }
    }
}
