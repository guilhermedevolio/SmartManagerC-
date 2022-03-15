using Microsoft.EntityFrameworkCore;
using SmartManager.Domain.Entities;
using SmartManager.Entities;
using SmartManager.Infra.Mappings;

namespace SmartManager.Infra.Context
{
    public class SmartManagerContext : DbContext {
        public SmartManagerContext()
        {}

        public SmartManagerContext(DbContextOptions<SmartManagerContext> options) : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("Server=localhost;Database=SmartManager;Uid=root;Pwd=;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
        }


        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<RefreshToken> RefreshTokens {get; set;}

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}