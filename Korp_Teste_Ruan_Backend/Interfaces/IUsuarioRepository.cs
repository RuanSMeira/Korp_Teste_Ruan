namespace Korp_Teste_Ruan_Backend.Interfaces;

using Korp_Teste_Ruan_Backend.Models;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<IEnumerable<Usuario>> GetByEmpresaIdAsync(int empresaId);
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario?> UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> EmpresaExistsAsync(int empresaId);
}