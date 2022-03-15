using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartManager.Domain.Entities;
using SmartManager.Entities;

namespace SmartManager.Infra.Mappings
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
               
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseMySqlIdentityColumn()
                   .HasColumnType("BIGINT");

            builder.Property(x => x.Token)
                   .IsRequired()
                   .HasColumnName("Token")
                   .HasColumnType("VARCHAR(255)");   

            builder.Property(x => x.Expires)
                   .IsRequired()
                   .HasColumnName("Expires")
                   .HasColumnType("DATETIME");   

            builder.Property(x => x.Revoked)
                   .IsRequired()
                   .HasColumnName("Revoked")
                   .HasColumnType("DATETIME");    

        }
    }
}