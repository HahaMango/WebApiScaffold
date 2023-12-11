using Mango.EntityFramework.Abstractions.Repositories;
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
    /// 用户角色关联表仓储实现
    /// </summary>
    public class UserRolesRepository : EfRepository<ImpDbContext, UserRoles>, IUserRolesRepository
    {
        public UserRolesRepository(ImpDbContext context) : base(context)
        {
        }
    }
}
