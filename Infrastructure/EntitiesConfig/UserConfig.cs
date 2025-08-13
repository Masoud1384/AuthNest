using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;  // your entity namespace

namespace Infrastructure.EntitiesConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.HasKey(x => x.UserId);
            b.Property(x => x.UserId).ValueGeneratedOnAdd(); 

            b.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            b.HasIndex(x => x.UserName).IsUnique();

            b.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false); 

            b.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            b.HasIndex(x => x.PhoneNumber)
                .IsUnique();

            b.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            b.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsUnicode(true);

            b.Property(x => x.Email)
                .HasMaxLength(254)
                .IsUnicode(false);

            b.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false);
        }
    }
}
