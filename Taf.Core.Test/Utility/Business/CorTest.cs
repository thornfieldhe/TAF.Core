// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CorTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   职责链测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;
using Xunit;

namespace Taf.Core.Test.Business
{
    /// <summary>
    /// CorTest 的摘要说明
    /// </summary>
    public class CorTest
    {
        /// <summary>
        /// 顺序执行
        /// </summary>
        [Fact]
        public void CorTest1()
        {
            BaseCoRHandler<Request> handler1 = new InternalHandler();
            BaseCoRHandler<Request> handler2 = new MailHandler();
            BaseCoRHandler<Request> handler3 = new DiscountHandler();
            BaseCoRHandler<Request> handler5 = new Regular2Handler();

            var request = new Request(20, "Mail");
            handler1.AddSuccessor(handler3);
            handler3.AddSuccessor(handler2);
            handler2.AddSuccessor(handler5);
            handler1.HandleRequest(request);
            Assert.Equal(20 *0.6*0.9* 1.3 * 10, request.Price);

        }

        /// <summary>
        /// 熔断短路
        /// </summary>
        [Fact]
        public void CorTest2()
        {
            BaseCoRHandler<Request> handler1 = new InternalHandler();
            BaseCoRHandler<Request> handler2 = new MailHandler();
            BaseCoRHandler<Request> handler3 = new DiscountHandler(){HasTerminatePoint = true};
            BaseCoRHandler<Request> handler4 = new RegularHandler();
            handler3.HasBreakPoint = true;
            handler3.Break += Handler1_Break;

            var request = new Request(20, "Mail");
    
            handler1.AddSuccessor(handler3);
            handler1.AddSuccessor(handler4);
            handler3.AddSuccessor(handler2);
            handler1.HandleRequest(request);
            Assert.Equal<double>(20*0.6*0.9, request.Price);
        }

        /// <summary>
        /// 链式执行
        /// </summary>
        [Fact]
        public void CorTest3()
        {
            BaseCoRHandler<Request> handler1 = new InternalHandler();
            BaseCoRHandler<Request> handler2 = new MailHandler();
            BaseCoRHandler<Request> handler3 = new DiscountHandler();
            BaseCoRHandler<Request> handler4 = new RegularHandler();
            BaseCoRHandler<Request> handler5 = new Regular2Handler();

            var request = new Request(20, "Mail");
            handler1.AddSuccessor(handler3);
            handler3.AddSuccessor(handler2);
            handler3.AddSuccessor(handler5);
            handler3.AddSuccessor(handler4);
            handler1.HandleRequest(request);
            Assert.Equal<double>(20*0.6*0.9 * 1.3 * 10 * 5, request.Price);

        }

        public static void Handler1_Break(object sender, CallHandlerEventArgs<Request> e)
        {
            Console.WriteLine(e.Request.Contex);
        }
    
}

    public class InternalHandler : BaseCoRHandler<Request>
    {
        protected override void Excute(Request request)
        {
            request.Price *= 0.6;
        }

        protected override bool AllowProcess(Request request) => true;
    }

    public class MailHandler : BaseCoRHandler<Request>
    {
        protected override void Excute(Request request)
        {
            request.Price *= 1.3;
        }

        protected override bool AllowProcess(Request request) => request.Contex == "Mail";
    }

    public class DiscountHandler : BaseCoRHandler<Request>
    {
        protected override void Excute(Request request)
        {
            request.Price *= 0.9;
        }

        protected override bool AllowProcess(Request request) => true;
    }

    public class RegularHandler : BaseCoRHandler<Request>
    {
        protected override void Excute(Request request)
        {
            request.Price *= 5;
        }

        protected override bool AllowProcess(Request request) => request.Contex.StartsWith("M");
    }

    public class Regular2Handler : BaseCoRHandler<Request>
    {
        protected override void Excute(Request request)
        {
            request.Price *= 10;
        }

        protected override bool AllowProcess(Request request) => request.Contex.Contains("Ma");
    }

    public class Request 
    {
        public double Price
        {
            get; set;
        }
        public string Contex
        {
            get; set;
        }

        public Request(double price, string type)
        {
            Price = price;
            Contex = type;
        }
    }
}
