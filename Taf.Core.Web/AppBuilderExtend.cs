// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InjectFilters.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Taf.Core.Extension;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// InjectFilters.cs

namespace Taf.Core.Web;

/// <summary>
/// 注入过滤器
/// </summary>
public static class AppBuilderExtend{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="otherOptionslter">添加其他过滤器</param>
    public static void AddFilters(this WebApplicationBuilder builder, Action<MvcOptions> otherOptions = null){
        builder.Services.Configure<ApiBehaviorOptions>(options => {
            options.SuppressModelStateInvalidFilter = true;//禁用系统的模型验证过滤器
        });
        builder.Services.AddControllers(options => {
            options.Filters.Add<LocalizationLangKeyFilter>(); //添加本地化语言处理
            options.Filters.Add<ModelValidFilter>();          //添加模型验证
            options.Filters.Add<EntityActionFilter>();        //添加结果包装
            options.Filters.Add<ExceptionFilter>();           //添加异常处理
            if(otherOptions != null){
                otherOptions(options);
            }
        });
       
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="singletonDependencyTypes">单例服务</param>
    /// <param name="transientDependencyTypes">瞬态服务</param>
    public static void AddDefaultService(
        this WebApplicationBuilder builder, List<Type> singletonDependencyTypes, List<Type> transientDependencyTypes){
        builder.Services.AddEndpointsApiExplorer();                                     //注入endPoint
        builder.Services.AddSwaggerGen();                                               //注入Swagger
        builder.AddLogger();                                                            //注入Serilog日志
        builder.Services.AddHttpClient();                                               //注入httpClient     
        builder.Services.AddHttpContextAccessor();                                      //注入Http上下文   
        builder.Services.AddInject(singletonDependencyTypes, transientDependencyTypes); //注入Application接口
        builder.AddCap();                                                               //注入CAP配置
        builder.AddRedis();                                                             //注入Redis配置
    }

    public static void UseDefaultService(this WebApplication app){
        if(app.Environment.IsDevelopment()){
            //使用swagger
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers(); //使用controller

        ServiceLocator.Instance.ServiceProvider = app.Services; //全局缓存注入服务,方便在任意地方使用注入对象    
    }

    /// <summary>
    /// 通过反射程序集获取系统需要的各类type对象
    /// </summary>
    /// <param name="allTypes">筛选Application,Domain程序集class对象</param>
    /// <returns></returns>
    public static (List<Type> DbEntityTypes, List<Type> SingletonDependencyTypes, List<Type> TransientDependencyTypes,
        List<IDataSeedContributor> DataSeedContributors) GetAllTypes(
            this WebApplicationBuilder builder, params Type[] allTypes){
        var dbEntityTypes            = new List<Type>();                 //定义数据库实体对象
        var singletonDependencyTypes = new List<Type>();                 //定义单例Application
        var transientDependencyTypes = new List<Type>();                 //定义瞬态Application
        var dataSeedContributors     = new List<IDataSeedContributor>(); //数据库初始化种子对象
        var defaultTypes             = new List<Type>(){ typeof(DataSeedContributor), typeof(LoginService) };
        var newTypes                 = allTypes.Union(defaultTypes);
        foreach(var classType in newTypes){
            foreach(var type in classType.Assembly.GetTypes()){
                if(typeof(DbEntity).IsAssignableFrom(type)
                && !type.IsAbstract){
                    dbEntityTypes.Add(type);
                } else if(typeof(IDataSeedContributor).IsAssignableFrom(type)
                       && !type.IsAbstract){
                    dataSeedContributors.Add(Activator.CreateInstance(type) as IDataSeedContributor);
                } else if(typeof(ISingletonDependency).IsAssignableFrom(type)
                       && !type.IsAbstract){
                    singletonDependencyTypes.Add(type);
                } else if(typeof(ITransientDependency).IsAssignableFrom(type)
                       && !type.IsAbstract){
                    transientDependencyTypes.Add(type);
                }
            }
        }

        return (dbEntityTypes, singletonDependencyTypes, transientDependencyTypes, dataSeedContributors);
    }
}
