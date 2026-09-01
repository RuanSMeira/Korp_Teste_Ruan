using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Repositories;

namespace Korp_Teste_Ruan_Backend.Services;

public class EmpresaService
{
    private readonly IEmpresaRepository _repository;

    public EmpresaService(IEmpresaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Empresa>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Empresa?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Empresa> CreateAsync(Empresa empresa)
    {
        ValidarCnpj(empresa.Cnpj);

        var existente = await _repository.GetByCnpjAsync(empresa.Cnpj);
        if (existente is not null)
            throw new InvalidOperationException($"Já existe uma empresa cadastrada com o CNPJ '{empresa.Cnpj}'.");

        return await _repository.AddAsync(empresa);
    }

    public async Task<Empresa?> UpdateAsync(int id, Empresa empresa)
    {
        if (id != empresa.EmpresaId)
        {
            throw new ArgumentException("O ID informado não corresponde ao ID da empresa.");
        }
        
        ValidarCnpj(empresa.Cnpj);

        var existeOutraComMesmoCnpj = await _repository.GetByCnpjAsync(empresa.Cnpj);

        if (existeOutraComMesmoCnpj is not null && existeOutraComMesmoCnpj.EmpresaId != id)
        {
            throw new InvalidOperationException($"Já existe outra empresa cadastrada com o CNPJ '{empresa.Cnpj}'.");
        }

        return await _repository.UpdateAsync(empresa);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static void ValidarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("O CNPJ é obrigatório.");

        var cnpjLimpo = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpjLimpo.Length != 14)
            throw new ArgumentException("O CNPJ deve conter 14 dígitos.");
    }
}