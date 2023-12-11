using Mango.EntityFramework.Repositories;
using Mango.Scaffold.Model.Entities.Users;
using Mango.Scaffold.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Repository
{
    /// <summary>
    /// 角色数据库表仓储实现
    /// </summary>
    public class RolesRepository : EfRepository<ImpDbContext, Roles>, IRolesRepository
    {
        public RolesRepository(ImpDbContext context) : base(context)
        {
        }
    }
}
