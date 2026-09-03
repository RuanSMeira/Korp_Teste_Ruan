using Korp_Teste_Ruan_Backend.DTOs.Request;
using Korp_Teste_Ruan_Backend.DTOs.Response;
using Korp_Teste_Ruan_Backend.Models;

namespace Korp_Teste_Ruan_Backend.Services.Interfaces;

public interface INotaFiscalService
{
    Task<NotaFiscalResponse> CreateComItensAsync(CriarNotaFiscalRequest request);
    Task<NotaFiscal?> GetByIdAsync(int id);
    Task<NotaFiscalResponse> EmitirNotaAsync(int notaFiscalId); 
}