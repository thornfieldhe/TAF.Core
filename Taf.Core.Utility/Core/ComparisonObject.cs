// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseExcuter.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   默认执行者
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Taf.Core.Utility.Core
{
    using System;

    /// <summary>
    /// 比较对象
    /// </summary>
    public class ComparisonObject<T, K> where T : class where K : class
    {
        /// <summary>
        /// 传入参与比较的2个对象
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="target">目标</param>
        public ComparisonObject(T source, K target)
        {
            Source    = source;
            Target    = target;
            Executors = new List<CompareAndExecutor<T, K>>();
        }

        /// <summary>
        /// 比较源
        /// </summary>
        public T Source { get; }

        /// <summary>
        /// 比较目标
        /// </summary>
        public K Target { get; }

        private List<CompareAndExecutor<T, K>> Executors { get; }

        /// <summary>
        /// 执行类似比较两个对象属性是否一致的操作
        /// </summary>
        /// <param name="sourceProperty"></param>
        /// <param name="targetProperty"></param>
        /// <typeparam name="TP"></typeparam>
        /// <example>
        /// public override bool AllowExcute(ComparisonObject<ProductOutput, OutputEditDto> comparison)
        ///{
        ///    return comparison.Compare(r => r.Name, s => s.Name);
        ///}
        ///</example>
        /// <returns></returns>
        public bool Compare<TP>(Func<T, TP> sourceProperty, Func<K, TP> targetProperty) 
        {
            return !sourceProperty(Source).Equals(targetProperty(Target));
        }

        /// <summary>
        /// 添加执行者
        /// </summary>
        /// <param name="executor"></param>
        public void AddExecutor(CompareAndExecutor<T, K> executor)
        {
            Executors.Add(executor);
        }

        /// <summary>
        /// 调用执行者执行任务
        /// </summary>
        public void Excute()
        {
            foreach(var executor in Executors)
            {
                if(executor.AllowExcute(this))
                {
                    executor.Execute(this);
                }
            }
        }
    }
}
