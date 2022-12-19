using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasColumnName("IdUsuario")
            .ValueGeneratedNever();

            builder.Property(e => e.LastName)
            .HasColumnName("Apellidos")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
            .HasColumnName("Email")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.BirthDate)
            .HasColumnName("FechaNacimiento")
            .HasColumnType("date");

            builder.Property(e => e.FirstName)
            .HasColumnName("Nombres")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Telephone)
            .HasColumnName("Telefono")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
            .HasColumnName("Activo");
        }
    }
}
