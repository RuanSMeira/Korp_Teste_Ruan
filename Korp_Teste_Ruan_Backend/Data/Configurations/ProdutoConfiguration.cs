using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Korp_Teste_Ruan_Backend.Models;

namespace Korp_Teste_Ruan_Backend.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.ProdutoId);

        builder.Property(p => p.Codigo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Saldo)
            .HasPrecision(18, 4);

        builder.Property(p => p.RowVersion)
            .IsRowVersion();

        // Código único por empresa
        builder.HasIndex(p => new { p.EmpresaId, p.Codigo })
            .IsUnique();

        builder.HasOne(p => p.Empresa)
            .WithMany(e => e.Produtos)
            .HasForeignKey(p => p.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
