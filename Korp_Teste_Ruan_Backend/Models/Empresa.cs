namespace Korp_Teste_Ruan_Backend.Models;

/// <summary>
/// Entidade que representa uma empresa cadastrada no sistema.
/// </summary>
public class Empresa
{
    public int EmpresaId { get; set; }
    public string RazaoSocial { get; set; } = string.Empty;
    public string NomeFantasia { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    // Navegação
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    public ICollection<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();
}
