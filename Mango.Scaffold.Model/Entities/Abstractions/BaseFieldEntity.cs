using Mango.EntityFramework.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Model.Entities.Abstractions
{
    /// <summary>
    /// 提供基础字段的Entity
    /// </summary>
    public class BaseFieldEntity : Entity
    {
        public override void SetId()
        {
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建自
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新自
        /// </summary>
        public string? UpdateBy { get; set; }
    }
}
