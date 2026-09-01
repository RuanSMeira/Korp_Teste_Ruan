namespace Korp_Teste_Ruan_Backend.Models;

/// <summary>
/// Entidade que representa um usuário do sistema, vinculado a uma empresa.
/// </summary>
public class Usuario
{
    public int UsuarioId { get; set; }
    public int EmpresaId { get; set; }
    public string NomeUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;

    // Navegação
    public Empresa Empresa { get; set; } = null!;
    public ICollection<NotaFiscal> NotasEmitidas { get; set; } = new List<NotaFiscal>();
}
