using Questao5.Infrastructure.CrossCutting;

namespace Questao5.Domain.Language;
public enum LanguageChoice
{
    Portuguese = 1,
    Ingles = 2,
    Espanhol = 3,
}
public class LanguageSystem : ILanguageSystem
{

    readonly IConfiguration _configuration;

    LanguageChoice _languageChoice;

    public LanguageSystem(IConfiguration configuration)
    {
        _configuration = configuration;
        ChoiceLanguage();
    }

    void ChoiceLanguage()
    {
        _languageChoice = _configuration.GetSection("Language").Value
                               .GetEnumToName(LanguageChoice.Portuguese);
    }

    public string SuccessMessage()
    {
        switch (_languageChoice)
        {
            case LanguageChoice.Portuguese:
                return "Dados Atualizados com Sucesso";
            case LanguageChoice.Ingles:
                return "Data Updated Successfully";
            case LanguageChoice.Espanhol:
                return "Datos actualizados con éxito";
            default:
                return "Dados Atualizados com Sucesso";
        }
    }

    public string ErrorMessage()
    {
        switch (_languageChoice)
        {
            case LanguageChoice.Portuguese:
                return "Houve Algum Problema.";
            case LanguageChoice.Ingles:
                return "There was a problem";
            case LanguageChoice.Espanhol:
                return "Había un problema";
            default:
                return "Dados Atualizados com Sucesso";
        }
    }

    public string InvalidAccount()
    {
        switch (_languageChoice)
        {
            case LanguageChoice.Portuguese:
                return "Atenção! conta corrente não cadastrada";
            case LanguageChoice.Ingles:
                return "Attention! not registered checking account";
            case LanguageChoice.Espanhol:
                return "¡Atención! cuenta corriente no registrada";
            default:
                return "Atenção! Conta corrente não cadastrada";
        }
    }

    public string InvalidValue()
    {
        switch (_languageChoice)
        {
            case LanguageChoice.Portuguese:
                return "Atenção! valor inválido";
            case LanguageChoice.Ingles:
                return "Attention! invalid value";
            case LanguageChoice.Espanhol:
                return "Atención! valor no válido";
            default:
                return "Atenção! valor inválido";
        }
    }

    public string InactiveAccount()
    {
        switch (_languageChoice)
        {
            case LanguageChoice.Portuguese:
                return "Atenção! conta inativa";
            case LanguageChoice.Ingles:
                return "Attention! inactive account";
            case LanguageChoice.Espanhol:
                return "¡Atención! cuenta inactiva";
            default:
                return "Atenção! conta inativa";
        }
    }

    public string InvalidType()
    {
        switch (_languageChoice)
        {
            case LanguageChoice.Portuguese:
                return "Atenção! Tipo da movimentação inválido";
            case LanguageChoice.Ingles:
                return "Warning! Invalid move type";
            case LanguageChoice.Espanhol:
                return "\r\n¡Atención! Tipo de movimiento no válido";
            default:
                return "Atenção! Tipo da movimentação inválido";
        }
    }
}