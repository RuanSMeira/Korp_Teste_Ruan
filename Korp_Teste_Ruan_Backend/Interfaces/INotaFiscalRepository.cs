namespace Korp_Teste_Ruan_Backend.Repositories;

using Korp_Teste_Ruan_Backend.Models;

public interface INotaFiscalRepository
{
    Task<IEnumerable<NotaFiscal>> GetAllAsync();
    Task<IEnumerable<NotaFiscal>> GetAllByEmpresaAsync(int empresaId);
    Task<NotaFiscal?> GetByIdAsync(int id);
    Task<NotaFiscal?> GetByIdComItensAsync(int id);
    Task<NotaFiscal> AddAsync(NotaFiscal notaFiscal);
    Task<bool> EmpresaExistsAsync(int empresaId);
    Task<bool> UsuarioExistsAsync(int usuarioId);
    Task<int> GetProximoNumeroSequencialAsync(int empresaId);
    Task<NotaFiscal> UpdateAsync(NotaFiscal notaFiscal);


}