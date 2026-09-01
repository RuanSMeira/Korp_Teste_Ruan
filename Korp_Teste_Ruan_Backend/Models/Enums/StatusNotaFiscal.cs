namespace Korp_Teste_Ruan_Backend.Models.Enums;

/// <summary>
/// Status de uma nota fiscal no ciclo de vida.
/// </summary>
public enum StatusNotaFiscal
{
    /// <summary>Nota fiscal criada, aguardando fechamento.</summary>
    Aberta = 0,
    /// <summary>Nota fiscal fechada, estoque já debitado.</summary>
    Fechada = 1
}
