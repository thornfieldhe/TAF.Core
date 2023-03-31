namespace Taf.Core.Utility
{
    /// <summary>
    /// 单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonBase<T> where T : new()
    {
        private static          T      instance;
        private static readonly object padlock = new();
        protected SingletonBase()
        {
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        } 
    }
}