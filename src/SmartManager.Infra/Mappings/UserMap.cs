using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartManager.Domain.Entities;

namespace SmartManager.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseMySqlIdentityColumn()
                   .HasColumnType("BIGINT");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("name")
                   .HasColumnType("VARCHAR(255)");
            
            
            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("email")
                   .HasColumnType("VARCHAR(255)");

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("password")
                   .HasColumnType("VARCHAR(255)");

        }
    }
}