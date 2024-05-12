using System.ComponentModel;

public class GetWeatherById
{
    /// <summary>
    /// Blah Blah
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Description("asdf")]
    public static string Invoke(string id)
    {
        return $"ID: {id}";
    }
}