using Mango.EntityFramework;
using Mango.EntityFramework.Abstractions;
using Mango.EntityFramework.BaseEntity;
using Mango.EntityFramework.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Scaffold.Repository
{
    /// <summary>
    /// DBContext
    /// </summary>
    public class ImpDbContext : BaseDbContext
    {
        public ImpDbContext(DbContextOptions op):base(op) { }

        protected override void OnEntityCreating(Type entityType, ModelBuilder modelBuilder)
        {
            if (entityType.BaseType == typeof(TimeFieldEntity))
            {
                //设置时间默认值
                modelBuilder.Entity(entityType.FullName)
                    .Property(nameof(TimeFieldEntity.CreateTime))
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity(entityType.FullName)
                    .Property(nameof(TimeFieldEntity.UpdateTime))
                    .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
