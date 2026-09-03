using System.Collections.Generic;

namespace Korp_Teste_Ruan_Backend.DTOs.Response;

public class NotaFiscalResponse
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int UsuarioEmissorId { get; set; }
    public long NumeroSequencial { get; set; }
    public string Status { get; set; } = string.Empty; // novo

    public List<ItemNotaFiscalResponse> Itens { get; set; } = new List<ItemNotaFiscalResponse>();
}
