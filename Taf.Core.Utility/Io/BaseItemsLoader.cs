// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseItemsLoader.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   从目录中加载数据到单例
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Taf.Core.Utility.Io
{
    using System;

    /// <summary>
    /// 从目录中加载数据到单例
    /// </summary>
    public class BaseItemsLoader<T, TK> : SingletonBase<T> where T : new()
    {
        /// <summary>
        /// 
        /// </summary>
        protected string ItemPath { get; set; }

        protected string ItmeFile { get; set; }

        /// <summary>
        /// 选项列表
        /// </summary>
        public virtual List<TK> Items { get;private set; }

        /// <summary>
        /// 加载系统选项和用户个人创建的选项
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName">文件名,支持通配符</param>
        public void Load(string path, string fileName)
        {
            ItemPath = path;
            ItmeFile = fileName;
            LoadItems();
        }


        /// <summary>
        /// 重新加载数据
        /// </summary>
        public virtual void Reload()
        {
            Load(ItemPath, ItmeFile);
        }

        /// <summary>
        /// 系统载入选择项
        /// </summary>
        protected virtual void LoadItems()
        {
            Items.AddRange(GetData());
        }

        /// <summary>
        /// 
        /// </summary>
        protected BaseItemsLoader()
        {
            Items = new List<TK>();
        }

        protected virtual List<TK> GetData()
        {
            var items = new List<TK>();
            var list  = Directory.GetFiles(ItemPath, ItmeFile);
            foreach(var item in list)
            {
                var infos = PathFileSerializer.JsonDeSerialize<List<TK>>(item);
                if(infos == null) continue;
                items.AddRange(infos);
            }

            return items;
        }
    }
}
