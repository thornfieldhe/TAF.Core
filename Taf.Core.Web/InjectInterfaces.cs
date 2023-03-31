// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InjectInterfaces.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Web
// InjectInterfaces.cs

namespace Taf.Core.Web;

/// <summary>
/// 默认单例/瞬时接口注入扩展
/// </summary>
public static class InjectInterfacesExt{
    public static void AddInject(
        this IServiceCollection services
      , List<Type>              singletonDependencyTypes
      , List<Type>              transientDependencyTypes){
        //注入ISingletonDependency接口
        foreach(var dependencyType in singletonDependencyTypes){
            var interfaces = dependencyType.GetInterfaces().Where(r => r.Name != typeof(ISingletonDependency).Name);
            foreach(var @interface in interfaces){
                services.AddSingleton(@interface, dependencyType);
            }
        }

        //注入ITransientDependency接口
        foreach(var transientDependencyType in transientDependencyTypes){
            var interfaces = transientDependencyType.GetInterfaces()
                                                    .Where(r => r.Name != typeof(ITransientDependency).Name);
            foreach(var @interface in interfaces){
                services.AddTransient(@interface, transientDependencyType);
            }
        }
    }
}
