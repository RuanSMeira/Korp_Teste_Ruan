using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Korp_Teste_Ruan_Backend.Models;

namespace Korp_Teste_Ruan_Backend.Data.Configurations;

public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
{
    public void Configure(EntityTypeBuilder<NotaFiscal> builder)
    {
        builder.HasKey(nf => nf.NotaFiscalId);

        builder.Property(nf => nf.Status)
            .HasConversion<int>();

        // Número sequencial único por empresa
        builder.HasIndex(nf => new { nf.EmpresaId, nf.NumeroSequencial })
            .IsUnique();

        builder.HasOne(nf => nf.Empresa)
            .WithMany(e => e.NotasFiscais)
            .HasForeignKey(nf => nf.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(nf => nf.UsuarioEmissor)
            .WithMany(u => u.NotasEmitidas)
            .HasForeignKey(nf => nf.UsuarioEmissorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
