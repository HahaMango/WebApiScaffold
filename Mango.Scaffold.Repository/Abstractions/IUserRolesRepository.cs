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
    /// 用户角色关联表仓储接口
    /// </summary>
    public interface IUserRolesRepository : IEfRepository<ImpDbContext, UserRoles>
    {
    }
}
