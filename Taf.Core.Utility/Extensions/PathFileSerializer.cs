﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathFileSerializer.cs" company="">
//   
// </copyright>
// <summary>
//   序列化对象到文件
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Taf.Core.Utility
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// 序列化对象到文件
    /// </summary>
    public class PathFileSerializer
    {
        /// <summary>
        /// 对象序列化成 XML 
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool XMLSerialize<T>(T obj, string path)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));

                path = path.Replace('\\', '/');
                var dir = string.Empty;
                if (path.LastIndexOf('/') != -1)
                {
                    dir = path.Substring(0, path.LastIndexOf('/'));
                }

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                xmlSerializer.Serialize(stream, obj);
                stream.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// XML 反序列化成对象
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        public static T XMLDeserialize<T>(string path)
        {
            var t = default(T);
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                if (!File.Exists(path))
                {
                    return default(T);
                }

                var stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                var obj = xmlSerializer.Deserialize(stream);
                t = (T)obj;
                stream.Close();
            }
            catch
            {
                return default(T);
            }

            return t;
        }

        /// <summary>
        /// Json序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool JsonSerialize<T>(T obj, string path)
        {
            try
            {
                path = path.Replace('\\', '/');
                var dir = string.Empty;
                if (path.LastIndexOf('/') != -1)
                {
                    dir = path.Substring(0, path.LastIndexOf('/'));
                }
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (var stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var json = JsonConvert.SerializeObject(obj);
                    var bytes = Encoding.UTF8.GetBytes(json);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Json文件反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T JsonDeSerialize<T>(string path)
        {
            var t = default(T);
            try
            {
                if (File.Exists(path))
                {
                    using (var stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        var bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        var json = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        t = JsonConvert.DeserializeObject<T>(json);
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
            return t;
        }
    }
}