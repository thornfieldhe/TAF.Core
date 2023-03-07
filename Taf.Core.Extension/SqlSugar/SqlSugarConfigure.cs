// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlSugarConfigureExternalService.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Coding4Fun.PluralizationServices;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Net.Utility
// SqlSugarConfigureExternalService.cs

namespace Taf.Core.Extension;

using System;

/// <summary>
/// sqlsugar config 
/// </summary>
public static class SqlSugarConfigure{
    public static ConfigureExternalServices GetDefaultConfig() =>
        new(){
            EntityService = (c, p) => {
                if(c.PropertyType.IsGenericType
                && c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)){
                    p.IsNullable = true;
                    if(c.PropertyType.GenericTypeArguments.Length == 1){
                        SeyDataType(c.PropertyType.GenericTypeArguments[0], p);
                    }
                } else if(c.PropertyType                            == typeof(string)
                       && c.GetCustomAttribute<RequiredAttribute>() == null){
                    //string类型如果没有Required isnullable=true
                    p.IsNullable = true;
                } else{
                    SeyDataType(c.PropertyType, p);
                }

                if(!string.IsNullOrEmpty(p.DbColumnName)
                && p.DbColumnName[0] >= 'A'
                && p.DbColumnName[0] <= 'Z'){
                    p.DbColumnName = c.Name.ToUnderLine();
                }

                if(c.Name.EndsWith("Id")
                && c.Name != "Id"){
                    //约定所有以Id结尾的属性为索引属性
                    if(p.IndexGroupNameList == null){
                        p.IndexGroupNameList = new[]{ c.Name.ToUnderLine() };
                    } else{
                        p.IndexGroupNameList = p.IndexGroupNameList.Union(new[]{ c.Name.ToUnderLine() }).ToArray();
                    }
                }
            }
          , EntityNameService = (type, entity) => {
                if(string.IsNullOrEmpty(entity.DbTableName)){
                    //未定义表名的对象,使用规则生成表名
                    entity.DbTableName =
                        $"business_{PluralizationService.CreateService(new CultureInfo("en")).Pluralize(type.Name).ToUnderLine()}";
                }

                entity.IsDisabledUpdateAll = true;
            }
        };

    /// <summary>
    /// 为默认数据结构设置数据库属性
    /// </summary>
    /// <param name="type"></param>
    /// <param name="columnInfo"></param>
    private static void SeyDataType(Type type, EntityColumnInfo columnInfo){
        if(!string.IsNullOrEmpty(columnInfo.DataType)){
            return;
        }

        //设置默认类型对应的数据库结构
        if(type == typeof(decimal)){
            columnInfo.DataType = "decimal(6,2)";
        } else if(type == typeof(Guid)){
            columnInfo.DataType = "char(36)";
        } else if(type.IsEnum){
            columnInfo.DataType = "int(4)";
        }
    }
}
