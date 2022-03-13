using Microsoft.EntityFrameworkCore;
using SmartManager.Domain.Entities;
using SmartManager.Infra.Mappings;

namespace SmartManager.Infra.Context
{
    public class SmartManagerContext : DbContext {
        public SmartManagerContext()
        {}

        public SmartManagerContext(DbContextOptions<SmartManagerContext> options) : base(options)
        {}


        public virtual DbSet<User> Users {get; set;}

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}