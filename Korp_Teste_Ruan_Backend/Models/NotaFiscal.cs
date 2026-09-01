using Korp_Teste_Ruan_Backend.Models.Enums;

namespace Korp_Teste_Ruan_Backend.Models;

/// <summary>
/// Entidade que representa uma nota fiscal emitida.
/// </summary>
public class NotaFiscal
{
    public int NotaFiscalId { get; set; }
    public int EmpresaId { get; set; }
    public int NumeroSequencial { get; set; }
    public StatusNotaFiscal Status { get; set; } = StatusNotaFiscal.Aberta;
    public int UsuarioEmissorId { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
    public DateTime? DataFechamento { get; set; }

    // Navegação
    public Empresa Empresa { get; set; } = null!;
    public Usuario UsuarioEmissor { get; set; } = null!;
    public ICollection<ItemNotaFiscal> Itens { get; set; } = new List<ItemNotaFiscal>();
}
