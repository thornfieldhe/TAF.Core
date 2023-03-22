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
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// SqlSugarBuilderExt.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// 添加默认启动配置
/// </summary>
public static class SqlSugarBuilderExt{
    private static List<TableFilterItem<object>> QueryFilters = new(); //默认数据库过滤器
    private static object                        _locker      = new(); //锁

    /// <summary>
    /// 添加SqlSurgar数据库依赖
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="dbName"></param>
    public static void AddMySql<DbContex>(
        this WebApplicationBuilder builder
      , List<Type>                 dbEntitTypes
      , string                     dbName = "MainConnection"
      ) where DbContex : TafDbContext, new(){
        var connection = builder.Configuration.GetConnectionString(dbName);
        var sqlSugar = new SqlSugarScope(
            new ConnectionConfig(){
                DbType                    = DbType.MySql
              , ConnectionString          = connection
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
                        entityInfo.SetValue(DateTime.UtcNow); //修改CreateTime字段
                    }

                    //update生效        
                    if(entityInfo.PropertyName  == "LastModificationTime"
                    && entityInfo.OperationType == DataFilterType.UpdateByObject){
                        entityInfo.SetValue(DateTime.UtcNow); //修改UpdateTime字段
                    }

                    //insert,update,delete生效        
                    if(entityInfo.PropertyName == "ConcurrencyStamp"){
                        entityInfo.SetValue(GuidGanerator.NextGuid().ToString("N")); //修改时间戳字段
                    }
                    //delete生效        
                    if(entityInfo.PropertyName  == "IsDeleted" 
                    &&  entityInfo.OperationType==DataFilterType.DeleteByObject){
                        entityInfo.SetValue(true); //修改IsDeleted字段
                    }    
                    if(entityInfo.PropertyName  == "DeletionTime" 
                    &&  entityInfo.OperationType==DataFilterType.DeleteByObject){
                        entityInfo.SetValue(DateTime.UtcNow); //修改DeletionTime字段
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

        ISugarUnitOfWork<DbContex> context = new SugarUnitOfWork<DbContex>(sqlSugar);
        builder.Services.AddSingleton<ISugarUnitOfWork<DbContex>>(context);
        builder.Services.AddSingleton<ISqlSugarClient>(sqlSugar);
        
        var defaultEntityTypes = typeof(IRepository<DbEntity>).AssemblyQualifiedName;
        foreach(var entityType in dbEntitTypes){
            var typeName = defaultEntityTypes.ToStr().As<IStringReg>().ReplaceReg(@"\[\[.*?\]\]", $"[[{entityType.AssemblyQualifiedName}]]", 0);
            builder.Services.AddTransient(Type.GetType(typeName)
                                        , Type.GetType(typeName.Replace("IRepository", "Repository")));
        }
        Log.Information("sqlSugar init Complited !");
    }

    private static void BindQueryFilter(ISqlSugarClient db){
        db.QueryFilter.AddTableFilter<ISoftDelete>(it => it.IsDeleted == false);
    }
}
