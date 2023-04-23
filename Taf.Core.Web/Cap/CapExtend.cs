// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CapExtend.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// 何翔华
// Taf.Core.Web
// CapExtend.cs

namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
public static class CapExtend{
    public static void AddCap(this WebApplicationBuilder builder){
        builder.Services.AddCap(x => {
            x.UseMySql(builder.Configuration["ConnectionStrings:CapConnection"]);
            x.UseRabbitMQ(o => {
                o.HostName = builder.Configuration["RabbitMq:HostName"];
                o.Port     = builder.Configuration["RabbitMq:Port"].ToInt();
                o.Password = builder.Configuration["RabbitMq:Password"];
                o.UserName = builder.Configuration["RabbitMq:UserName"];
            });
        });
    }
}