namespace Taf.Core
{
    using System;

    /// <summary>
    /// 职责链响应断点的事件参数
    /// </summary>
    public class CallHandlerEventArgs<T> : EventArgs
    {

        public BaseCoRHandler<T> Handler
        {
            get; private set;
        }
        public T Request
        {
            get; private set;
        }

        public CallHandlerEventArgs(BaseCoRHandler<T> handler, T request)
        {
            this.Handler = handler;
            this.Request = request;
        }
    }
}