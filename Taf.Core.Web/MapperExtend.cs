// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperExtend.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mapster;
using MapsterMapper;
using System.Collections.Generic;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// MapperExtend.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// 对象映射注入
/// </summary>
public static class MapperExtend{
    /// <summary>
    /// 使用对象映射
    /// </summary>
    /// <param name="services"></param>
    /// <param name="addMap"></param>
    public static void AddMap(this IServiceCollection services, Action<TypeAdapterConfig> addMap){
        var config = new TypeAdapterConfig();
        addMap.IfNotNull((s) => s(config));
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
    }
}
