// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlSugarBuilderExt.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Serilog;
using SqlSugar;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using Taf.Core.Extension;

// 何翔华
// Taf.Core.Web
// SqlSugarBuilderExt.cs

namespace Taf.Core.Web.SqlSugar;

using System;

/// <summary>
/// 添加默认启动配置
/// </summary>
public static class SqlSugarBuilderExt{
    private static List<Type>                    entityTypes;
    private static List<TableFilterItem<object>> QueryFilters = new(); //默认数据库过滤器
    private static object                        _locker      = new(); //锁

    /// <summary>
    /// 添加SqlSurgar数据库依赖
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="dbName"></param>
    public static void UseSqlsugarForMySQL(this WebApplicationBuilder app, string dbName = "MainConnection"){
        var sqlSugar = new SqlSugarScope(
            new ConnectionConfig(){
                DbType                    = DbType.MySql
              , ConnectionString          = app.Configuration.GetConnectionString(dbName)
              , IsAutoCloseConnection     = true
              , ConfigureExternalServices = SqlSugarConfigure.GetDefaultConfig()
            }
          , db => {
                BindQueryFilter(db);
                //单例参数配置，所有上下文生效
                db.Aop.OnLogExecuting = (sql, pars) => {
                    Log.Debug(new string('-', 100));  //输出sql
                    Log.Debug("[Debug] Sql: " + sql); //输出sql
                    Log.Debug("[Debug] Parameters:"
                            + string.Join("      ", pars.Select(r => $"{r.ParameterName}:{r.Value}"))); //输出sql
                };

                db.Aop.DataExecuting = (oldValue, entityInfo) => {
                    //inset生效
                    if(entityInfo.PropertyName  == "CreationTime"
                    && entityInfo.OperationType == DataFilterType.InsertByObject){
                        entityInfo.SetValue(DateTime.Now); //修改CreateTime字段
                    }

                    //update生效        
                    if(entityInfo.PropertyName  == "LastModificationTime"
                    && entityInfo.OperationType == DataFilterType.UpdateByObject){
                        entityInfo.SetValue(DateTime.Now); //修改UpdateTime字段
                    }

                    //insert,update生效        
                    if(entityInfo.PropertyName == "ConcurrencyStamp"){
                        entityInfo.SetValue(GuidGanerator.NextGuid().ToString("N")); //修改时间戳字段
                    }
                };

                db.Aop.OnLogExecuted = (sql, p) => {
                    //执行时间超过1秒
                    if(db.Ado.SqlExecutionTime.TotalSeconds > 1){
                        //代码CS文件名
                        var fileName = db.Ado.SqlStackTrace.FirstFileName;
                        //代码行数
                        var fileLine = db.Ado.SqlStackTrace.FirstLine;
                        //方法名
                        var firstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                        //db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息

                        Log.Information(
                            $"[Warn] SQL :语句执行时间超时:堆栈信息:fileName:{fileName},rowNum:{fileLine},methord:{firstMethodName}");
                    }
                    //相当于EF的 PrintToMiniProfiler
                };
            });

        app.Services.AddSingleton<ISqlSugarClient>(sqlSugar);
        Log.Information("sqlSugar init Complited !");
    }

    private static void BindQueryFilter(ISqlSugarClient db){
        db.QueryFilter.AddTableFilter<ISoftDelete>(it => it.IsDeleted == false);
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="assemblyFiles"></param>
    public static void InitDatabase(string connection, params string[] assemblyFiles){
        var db = new SqlSugarClient(new ConnectionConfig{
            ConnectionString          = connection
          , DbType                    = DbType.MySql //必填   
          , IsAutoCloseConnection     = true
          , ConfigureExternalServices = SqlSugarConfigure.GetDefaultConfig()
        });

        db.DbMaintenance.CreateDatabase();
        var types = new List<Type>();
        foreach(var assemblyFile in assemblyFiles){
            types.AddRange(Assembly
                          .LoadFrom(assemblyFile)
                          .GetTypes().Where(s => typeof(DbEntity).IsAssignableFrom(s)));
        }

        db.CodeFirst.SetStringDefaultLength(200).InitTables(types.ToArray());
        Trace.TraceInformation("init database complited !");
    }
}
