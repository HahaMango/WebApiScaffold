using Mango.EntityFramework.Abstractions.Repositories;
using Mango.Scaffold.Model.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Repository.Abstractions
{
    /// <summary>
    /// 角色数据库表仓储对象
    /// </summary>
    public interface IRolesRepository : IEfRepository<ImpDbContext, Roles>
    {
    }
}
