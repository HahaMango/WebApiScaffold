using Mango.EntityFramework.Abstractions.Repositories;
using Mango.EntityFramework.Repositories;
using Mango.Scaffold.Model.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Repository.Abstractions
{
    /// <summary>
    /// Users表仓储接口
    /// </summary>
    public interface IUsersRepository : IEfRepository<ImpDbContext, Users>
    {
    }
}
