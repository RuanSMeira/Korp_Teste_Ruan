using Korp_Teste_Ruan_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Korp_Teste_Ruan_Backend.Data.Configurations;

public class MovimentacaoEstoqueConfiguration : IEntityTypeConfiguration<MovimentacaoEstoque>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
    {
        builder.HasKey(m => m.MovimentacaoEstoqueId);
        builder.Property(m => m.Quantidade).HasPrecision(18, 4);
        builder.Property(m => m.SaldoAnterior).HasPrecision(18, 4);
        builder.Property(m => m.SaldoPosterior).HasPrecision(18, 4);
        builder.Property(m => m.Observacao).HasMaxLength(500);
        builder.HasOne(m => m.Empresa).WithMany().HasForeignKey(m => m.EmpresaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(m => m.Produto).WithMany().HasForeignKey(m => m.ProdutoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(m => m.Usuario).WithMany().HasForeignKey(m => m.UsuarioId).OnDelete(DeleteBehavior.Restrict);
    }
}