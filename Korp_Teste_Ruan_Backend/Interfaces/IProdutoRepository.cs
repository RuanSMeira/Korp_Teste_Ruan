namespace Korp_Teste_Ruan_Backend.Repositories;

using Korp_Teste_Ruan_Backend.Models;

public interface IProdutoRepository
{
	Task<IEnumerable<Produto>> GetAllAsync();
	Task<IEnumerable<Produto>> GetByEmpresaIdAsync(int empresaId);
	Task<Produto?> GetByIdAsync(int id);
	Task<Produto?> GetByCodigoAsync(int empresaId, string codigo);
	Task<Produto> AddAsync(Produto produto);
	Task<Produto?> UpdateAsync(Produto produto);
	Task<bool> DeleteAsync(int id);
	Task<bool> ExistsAsync(int id);
	Task<bool> EmpresaExistsAsync(int empresaId);
}