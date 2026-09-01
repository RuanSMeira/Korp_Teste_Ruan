using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Korp_Teste_Ruan_Backend.Models;

namespace Korp_Teste_Ruan_Backend.Data.Configurations;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(e => e.EmpresaId);

        builder.Property(e => e.RazaoSocial)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.NomeFantasia)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Cnpj)
            .IsRequired()
            .HasMaxLength(14);

        builder.HasIndex(e => e.Cnpj)
            .IsUnique();
    }
}
