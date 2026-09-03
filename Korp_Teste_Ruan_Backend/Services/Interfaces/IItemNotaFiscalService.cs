using Korp_Teste_Ruan_Backend.Models;

namespace Korp_Teste_Ruan_Backend.Services.Interfaces;

public interface IItemNotaFiscalService
{
    Task<ItemNotaFiscal> CreateAsync(ItemNotaFiscal item);
}
