using System;
using System.Collections.Generic;

namespace Taf.Core.Utility{
    public abstract class BaseCoRHandler<Request>{
        protected BaseCoRHandler(){
            Successors       ??= new List<BaseCoRHandler<Request>>();
        }

        protected abstract bool AllowProcess(Request request);
        protected abstract void Excute(Request       request);

        private bool _isTeminate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public void Process(Request request){
            if(!_isTeminate
            && AllowProcess(request)){
                Excute(request);
                if(!HasTerminatePoint){
                    foreach(var successor in Successors){
                        successor.HandleRequest(request);
                    }
                } else{
                    var topHandler = FindTopHandler();
                    topHandler.TerminateCall();
                }
            }
        }

        /// <summary>
        /// 处理客户请求
        /// </summary>
        /// <param name="request"></param>
        public virtual void HandleRequest(Request request){
            if(HasBreakPoint){
                OnBreak(new CallHandlerEventArgs<Request>(this, request));
            }

            if(request == null){
                return;
            }

            Process(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argus"></param>
        protected virtual void OnBreak(CallHandlerEventArgs<Request> argus){
            Break?.Invoke(this, argus);
        }

        /// <summary>
        /// 添加后续节点
        /// </summary>
        /// <param name="success"></param>
        public void AddSuccessor(BaseCoRHandler<Request> success){
            Successors.Add(success);
            success.Source = this;
        }

        public BaseCoRHandler<Request>? Source{ get; set; }

        /// <summary>
        /// 实现迭代器，并且对容器对象实现隐性递归
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseCoRHandler<Request>> Enumerate(){
            yield return this;
            if((Successors.Count <= 0)) yield break;
            foreach(var child in Successors){
                foreach(var item in child.Enumerate())
                    yield return item;
            }
        }

        /// <summary>
        /// 后继节点
        /// </summary>
        public List<BaseCoRHandler<Request>> Successors{ get; set; }

        /// <summary>
        /// 是否定义断点
        /// </summary>
        public bool HasBreakPoint{ get; set; }
        
        /// <summary>
        /// 是否定义断点
        /// </summary>
        public bool HasTerminatePoint{ get; set; }

        /// <summary>
        /// 断点事件
        /// </summary>
        public event EventHandler<CallHandlerEventArgs<Request>> Break;

        protected virtual void TerminateCall(){
            _isTeminate = true;
            Fx.If(Successors?.Count > 0)
              .Then(() => {
                   foreach(var successor in Successors){
                       successor.TerminateCall();
                   }
               });
        }

        private BaseCoRHandler<Request> FindTopHandler(){
            if (Source==null){
                return this;
            }
            return Source.FindTopHandler();
        } 
    }
}
