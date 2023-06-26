namespace Questao5.Infrastructure.CrossCutting;
public static class CommonMethodsExtensions
{
    public static T GetEnumToName<T>(this string value, T def) => CommonMethods.GetEnumToName<T>(value, def);
}
