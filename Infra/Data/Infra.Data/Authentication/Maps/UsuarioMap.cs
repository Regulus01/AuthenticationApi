using Domain.Authentication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Authentication.Maps;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("Usu_Id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Email)
            .HasColumnName("Usu_Email")
            .IsRequired();

        builder.Property(x => x.DataDeCadastro)
            .HasColumnName("Usu_DataDeCadastro")
            .IsRequired();

        builder.Property(x => x.UltimoLogin)
            .HasColumnName("Usu_UltimoLogin");
        
        builder.Property(x => x.Status)
            .HasColumnName("Usu_Status")
            .IsRequired();
        
        builder.Property(x => x.Password)
            .HasColumnName("Usu_Password")
            .IsRequired();
        
        builder.ToTable("Usuario", "Authentication");
    }
}