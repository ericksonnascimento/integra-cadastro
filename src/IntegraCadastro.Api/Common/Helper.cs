using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntegraCadastro.Api.Common;

public class Helper
{
    public static bool IsValidJson(object? value)
    {
        return value is JObject;
    }

    public static string? ToJsonString(object? value)
    {
        switch (value)
        {
            case null:
                return null;
            case string str:
                try
                {
                    JToken.Parse(str);
                    return str;
                }
                catch (JsonReaderException)
                {
                    return null;
                }

                break;
            default:
                try
                {
                    return JsonConvert.SerializeObject(value);
                }
                catch
                {
                    return null;
                }

                break;
        }
    }
}