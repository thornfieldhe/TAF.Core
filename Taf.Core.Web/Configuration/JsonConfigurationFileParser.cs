// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonConfigurationFileParser.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json.Linq;
using System.Globalization;

// 何翔华
// Taf.Core.Web
// JsonConfigurationFileParser.cs

namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
public class JsonConfigurationFileParser{
    private readonly IDictionary<string, string> _data =
        new SortedDictionary<string, string>(
            StringComparer.OrdinalIgnoreCase);
    private readonly Stack<string> _context = new();
    private          string        _currentPath;
    private          string        jsonText;

    private JsonConfigurationFileParser(string jsonText) => this.jsonText = jsonText;

    public static IDictionary<string, string> Parse(string jsonText) =>
        new JsonConfigurationFileParser(jsonText).ParseStream();

    private IDictionary<string, string> ParseStream(){
        _data.Clear();
        VisitJObject(JObject.Parse(jsonText));
        return _data;
    }

    private void VisitJObject(JObject jObject){
        foreach(JProperty property in jObject.Properties()){
            EnterContext(property.Name);
            VisitProperty(property);
            ExitContext();
        }
    }

    private void VisitProperty(JProperty property) => VisitToken(property.Value);

    private void VisitToken(JToken token){
        switch(token.Type){
            case JTokenType.Object:
                VisitJObject(token.Value<JObject>());
                break;
            case JTokenType.Array:
                VisitArray(token.Value<JArray>());
                break;
            case JTokenType.Integer:
            case JTokenType.Float:
            case JTokenType.String:
            case JTokenType.Boolean:
            case JTokenType.Null:
            case JTokenType.Raw:
            case JTokenType.Bytes:
                VisitPrimitive(token.Value<JValue>());
                break;
            default:
                throw new FormatException("JToken is error");
        }
    }

    private void VisitArray(JArray array){
        for(int index = 0; index < array.Count; ++index){
            EnterContext(index.ToString());
            VisitToken(array[index]);
            ExitContext();
        }
    }

    private void VisitPrimitive(JValue data){
        string currentPath = _currentPath;
        if(_data.ContainsKey(currentPath))
            throw new FormatException("JValue is Error");
        _data[currentPath] = data.ToString(CultureInfo.InvariantCulture);
    }

    private void EnterContext(string context){
        _context.Push(context);
        _currentPath = ConfigurationPath.Combine(_context.Reverse());
    }

    private void ExitContext(){
        _context.Pop();
        _currentPath = ConfigurationPath.Combine(_context.Reverse());
    }
}
