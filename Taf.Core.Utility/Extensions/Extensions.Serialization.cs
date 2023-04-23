// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Serialization.cs" company="">
//   
// </copyright>
// <summary>
//   序列化扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Xml.Serialization;

namespace Taf.Core.Utility{
    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions{
        /// <summary>
        /// The default formatter type.
        /// </summary>

        private static readonly Dictionary<string, object> _dic = new();

        /// <summary>
        /// 将源对象所有属性赋值给目标对象,【仅仅支持系统基本类型，不支持对象】
        /// 确保目标对象的属性名称与原对象的属性名称一致，且目标对象属性数量可以少于原对象属性数量
        /// 【Dictionary克隆使用Dictionary<string, int> copy = new Dictionary<string, int>(dictionary);】
        /// 对象内部包含的对象需要分别使用Copy方法进行克隆
        /// </summary>
        /// <param name="tIn"></param>
        /// <param name="skipProperties"></param>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <returns></returns>
        public static TOut Clone<TIn, TOut>(this TIn tIn, params string[] skipProperties){
            var skipKey =  $"_{string.Join('_', skipProperties)}";
            var key     = $"trans_exp_{typeof(TIn).FullName}_{typeof(TOut).FullName}_{skipKey}";
            if(!_dic.ContainsKey(key)){
                var parameterExpression = Expression.Parameter(typeof(TIn), "p");
                var memberBindingList   = new List<MemberBinding>();

                foreach(var item in typeof(TOut).GetProperties()){
                    if(!item.CanWrite
                    || skipProperties.Contains(item.Name))
                        continue;
                    var property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                var memberInitExpression =
                    Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                var lambda =
                    Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, parameterExpression);
                var func = lambda.Compile();

                _dic[key] = func;
            }

            return ((Func<TIn, TOut>) _dic[key])(tIn);
        }

        #region Xml序列化

        /// <summary>
        /// 序列化实例到Xml
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string XmlSerializer<T>(this T obj){
            var  stream = new MemoryStream();
            var xml    = new XmlSerializer(typeof(T));
            try{
                // 序列化对象
                xml.Serialize(stream, obj);
            } catch(InvalidOperationException ex){
                throw ex;
            }

            stream.Position = 0;
            var sr  = new StreamReader(stream);
            var       str = sr.ReadToEnd();
            sr.Dispose();
            stream.Dispose();
            return str;
        }

        /// <summary>
        /// 反序列化Xml到实例
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="xml">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T XmlDeserializeFromString<T>(this string xml){
            try{
                if(xml != null){
                    using(var sr = new StringReader(xml)){
                        var xmldes = new XmlSerializer(typeof(T));
                        return (T) xmldes.Deserialize(sr);
                    }
                }

                return default(T);
            } catch(Exception ex){
                throw ex;
            }
        }

        #endregion


        #region Json序列化

        ///<summary>
        /// json反序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T DeSerializesFromString<T>(string jsonString) where T : class => JsonSerializer.Deserialize<T>(jsonString);

        /// <summary>
        /// json序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string SerializeToString<T>(T t) => JsonSerializer.Serialize(t);

    #endregion
    }
}
