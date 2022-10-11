namespace Taf.Core.Utility
{
    /// <summary>
    /// 比较两个对象的某些属性是否一致,如果不一致则对源对象进行一系列的操作
    /// </summary>
    public abstract class CompareAndExecutor<T, K> where T : class where K : class
    {
        /// <summary>
        /// 是否执行
        /// </summary>
        /// <returns></returns>
        public abstract bool AllowExcute(ComparisonObject<T, K> comparison);

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="comparison"></param>
        public abstract void Execute(ComparisonObject<T, K> comparison);
    }
}