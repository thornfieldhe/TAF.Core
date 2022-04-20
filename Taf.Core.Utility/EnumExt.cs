﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExt.cs" company="">
//   
// </copyright>
// <summary>
//   枚举操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Taf.Core.Utility{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Utility;

    /// <summary>
    /// 枚举操作
    /// </summary>
    public static class EnumExt{
    #region GetInstance(获取实例)

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T">
        /// 枚举
        /// </typeparam>
        /// <param name="member">
        /// 成员名或值,
        /// 范例:Enum1枚举有成员A=0,则传入"A"或"0"获取 Enum1.A
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T GetInstance<T>(object member){
            string value = member.ToStr();
            if(string.IsNullOrWhiteSpace(value)){
                throw new ArgumentNullException("member");
            }

            return (T)System.Enum.Parse(Reflection.GetType<T>(), value, true);
        }

    #endregion

    #region GetName(获取成员名)

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <typeparam name="T">
        /// 枚举
        /// </typeparam>
        /// <param name="member">
        /// 成员名、值、实例均可,
        /// 范例:Enum1枚举有成员A=0,则传入Enum1.A或0,获取成员名"A"
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetName<T>(object member){
            return GetName(Reflection.GetType<T>(), member);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="type">
        /// 枚举类型
        /// </param>
        /// <param name="member">
        /// 成员名、值、实例均可
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetName(Type type, object member){
            if(type == null){
                return string.Empty;
            }

            if(member == null){
                return string.Empty;
            }

            if(member is string){
                return member.ToString();
            }

            if(type.IsEnum == false){
                return string.Empty;
            }

            return System.Enum.GetName(type, member);
        }

    #endregion

    #region GetValue(获取成员值)

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="T">
        /// 枚举
        /// </typeparam>
        /// <param name="member">
        /// 成员名、值、实例均可，
        /// 范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetValue<T>(object member){
            return GetValue(Reflection.GetType<T>(), member);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="type">
        /// 枚举类型
        /// </param>
        /// <param name="member">
        /// 成员名、值、实例均可
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetValue(Type type, object member){
            var value = member.ToStr();
            if(string.IsNullOrWhiteSpace(value)){
                throw new ArgumentNullException("member");
            }

            return (int)System.Enum.Parse(type, member.ToString(), true);
        }

    #endregion

    #region GetMemberDescription(获取描述)

        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <typeparam name="T">
        /// 枚举
        /// </typeparam>
        /// <param name="member">
        /// 成员名、值、实例均可
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDescription<T>(object member){
            return Reflection.GetFieldDescription<T>(GetName<T>(member));
        }

        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="type">
        /// 枚举类型
        /// </param>
        /// <param name="member">
        /// 成员名、值、实例均可
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDescription(Type type, object member){
            return Reflection.GetFiledDescription(type, GetName(type, member));
        }

    #endregion

    #region GetItems(获取描述项集合)

        /// <summary>
        /// 获取描述项集合,文本设置为Description，值为Value
        /// </summary>
        /// <typeparam name="T">
        /// 枚举类型
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static List<Item> GetItems<T>(){
            var enumType = Reflection.GetType<T>();
            ValidationIsEnum(enumType);
            var result = new List<Item>();
            foreach(var field in enumType.GetFields()){
                AddItem<T>(result, field, enumType);
            }

            return result.OrderBy(t => t.SortId).ToList();
        }

        /// <summary>
        /// 添加描述项
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <param name="enumType">
        /// The enum Type.
        /// </param>
        private static void AddItem<T>(ICollection<Item> result, FieldInfo field, Type enumType){
            if(!field.FieldType.IsEnum){
                return;
            }

            var value       = GetValue<T>(field, enumType);
            var description = Reflection.GetMemberDescription(enumType, field);
            var sortId      = GetSortId(field);
            if(sortId == -1){
                sortId = value;
            }

            result.Add(new Item(description, value.ToString(), sortId));
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">
        /// 枚举类型
        /// </typeparam>
        /// <param name="field">
        /// 枚举字段
        /// </param>
        /// <param name="enumType">
        /// 枚举类型
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int GetValue<T>(FieldInfo field, Type enumType){
            var memberValue = enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
            return GetValue<T>(memberValue);
        }

        /// <summary>
        /// 获取排序号
        /// </summary>
        /// <param name="field">
        /// 枚举字段
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int GetSortId(FieldInfo field){
            var attribute = field.GetCustomAttributes(typeof(OrderByAttribute), true).FirstOrDefault();
            if(attribute == null){
                return -1;
            }

            return ((OrderByAttribute)attribute).SortId;
        }

        /// <summary>
        /// 验证是否枚举类型
        /// </summary>
        private static void ValidationIsEnum(Type enumType){
            if(enumType.IsEnum == false)
                throw new InvalidOperationException(String.Format("类型 {0} 不是枚举", enumType));
        }

    #endregion

        /// <summary>
        /// 是否是可空枚举
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullableEnum(this Type t){
            var u = Nullable.GetUnderlyingType(t);
            return u is{ IsEnum: true };
        }
    }
}
