using Mango.EntityFramework;
using Mango.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Repository
{
    /// <summary>
    /// DBContext
    /// </summary>
    public class ImpDbContext : BaseDbContext
    {
        public ImpDbContext() { }

        public ImpDbContext(DbContextOptions op):base(op) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
