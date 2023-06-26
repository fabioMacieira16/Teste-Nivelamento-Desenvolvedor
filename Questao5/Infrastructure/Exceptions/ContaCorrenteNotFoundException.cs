
namespace Questao5.Infrastructure.Exceptions;

public class ContaCorrenteNotFoundException : IOException
{
    public ContaCorrenteNotFoundException(int contaCorrenteId)
        : base($"Não foi encontrada uma conta corrente com o id {contaCorrenteId}")
    { }

}