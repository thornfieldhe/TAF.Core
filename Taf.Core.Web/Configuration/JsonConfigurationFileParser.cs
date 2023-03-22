// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonConfigurationFileParser.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

// 何翔华
// Taf.Core.Web
// JsonConfigurationFileParser.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// 
/// </summary>
public class JsonConfigurationFileParser{
    private readonly IDictionary<string, string> _data =
        (IDictionary<string, string>)new SortedDictionary<string, string>(
            (IComparer<string>)StringComparer.OrdinalIgnoreCase);
    private readonly Stack<string> _context = new();
    private          string        _currentPath;
    private          string        jsonText;

    private JsonConfigurationFileParser(string jsonText){
        this.jsonText = jsonText;
    }

    public static IDictionary<string, string> Parse(string jsonText) =>
        new JsonConfigurationFileParser(jsonText).ParseStream();

    private IDictionary<string, string> ParseStream(){
        this._data.Clear();
        this.VisitJObject(JObject.Parse(jsonText));
        return this._data;
    }

    private void VisitJObject(JObject jObject){
        foreach(JProperty property in jObject.Properties()){
            this.EnterContext(property.Name);
            this.VisitProperty(property);
            this.ExitContext();
        }
    }

    private void VisitProperty(JProperty property) => this.VisitToken(property.Value);

    private void VisitToken(JToken token){
        switch(token.Type){
            case JTokenType.Object:
                this.VisitJObject(token.Value<JObject>());
                break;
            case JTokenType.Array:
                this.VisitArray(token.Value<JArray>());
                break;
            case JTokenType.Integer:
            case JTokenType.Float:
            case JTokenType.String:
            case JTokenType.Boolean:
            case JTokenType.Null:
            case JTokenType.Raw:
            case JTokenType.Bytes:
                this.VisitPrimitive(token.Value<JValue>());
                break;
            default:
                throw new FormatException("JToken is error");
        }
    }

    private void VisitArray(JArray array){
        for(int index = 0; index < array.Count; ++index){
            this.EnterContext(index.ToString());
            this.VisitToken(array[index]);
            this.ExitContext();
        }
    }

    private void VisitPrimitive(JValue data){
        string currentPath = this._currentPath;
        if(this._data.ContainsKey(currentPath))
            throw new FormatException("JValue is Error");
        this._data[currentPath] = data.ToString((IFormatProvider)CultureInfo.InvariantCulture);
    }

    private void EnterContext(string context){
        this._context.Push(context);
        this._currentPath = ConfigurationPath.Combine(this._context.Reverse<string>());
    }

    private void ExitContext(){
        this._context.Pop();
        this._currentPath = ConfigurationPath.Combine(this._context.Reverse<string>());
    }
}
