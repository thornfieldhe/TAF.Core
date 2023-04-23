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

// 何翔华
// Taf.Core.Web
// MapperExtend.cs

namespace Taf.Core.Web;

/// <summary>
/// 对象映射注入
/// </summary>
public static class MapperExtend{
    /// <summary>
    /// 使用对象映射
    /// </summary>
    /// <param name="services"></param>
    /// <param name="addMap"></param>
    public static void AddEntityMap(this WebApplicationBuilder builder, Action<TypeAdapterConfig> addMap){
        var config = new TypeAdapterConfig();
        addMap.IfNotNull(s => s(config));
        builder.Services.AddSingleton(config);
        builder.Services.AddSingleton<IMapper, ServiceMapper>();
    }
}
