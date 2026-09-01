namespace Korp_Teste_Ruan_Backend.Exceptions;

/// <summary>
/// Exceção customizada para erros de regra de negócio.
/// Retorna HTTP 422 Unprocessable Entity quando não tratada por handler específico.
/// </summary>
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
    public BusinessException(string message, Exception innerException) : base(message, innerException) { }
}
