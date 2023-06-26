namespace Questao5.Infrastructure.CrossCutting;
public class CommonMethods
{
    public static T GetEnumToName<T>(string value, T defaultValue)
    {
        Enum.TryParse(typeof(T), value, true, out object res);
        if (res == null)
            return defaultValue;
        if (Enum.IsDefined(typeof(T), res))
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        return defaultValue;
    }
}
