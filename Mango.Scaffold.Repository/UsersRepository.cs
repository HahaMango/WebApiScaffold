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
    public class UsersRepository : EfRepository<ImpDbContext, Users>, IUsersRepository
    {
        public UsersRepository(ImpDbContext context) : base(context)
        {
        }
    }
}
