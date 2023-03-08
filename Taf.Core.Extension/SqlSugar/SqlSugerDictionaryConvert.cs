// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlSugerDictionaryConvert.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SqlSugar;
using System.Collections.Generic;
using System.Data;

// 何翔华
// Taf.Core.Web
// SqlSugerDictionaryConvert.cs

namespace Taf.Core.Extension;

using System;

/// <summary>
/// Dictionary转换器
/// </summary>
public class SqlSugerDictionaryConvert: ISugarDataConverter{
    public SugarParameter ParameterConverter<T>(object value, int i)
    {
        var name = "@Dic" + i;
        var str  = new SerializeService().SerializeObject(value);
        return new SugarParameter(name, str);
    }

    public T QueryConverter<T>(IDataRecord dataRecord, int dataRecordIndex){
        var str = dataRecord.GetValue(dataRecordIndex) + "";
        return new SerializeService().DeserializeObject<T>(str);
    } 
}
