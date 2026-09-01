namespace Korp_Teste_Ruan_Backend.Repositories;

using Korp_Teste_Ruan_Backend.Models;

public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> GetAllAsync();
    Task<Empresa?> GetByIdAsync(int id);
    Task<Empresa?> GetByCnpjAsync(string cnpj);
    Task<Empresa> AddAsync(Empresa empresa);
    Task<Empresa?> UpdateAsync(Empresa empresa);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}