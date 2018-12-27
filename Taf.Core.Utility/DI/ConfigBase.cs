//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="ConfigBase.cs" company="">
////   
//// </copyright>
//// <summary>
////   Ioc配置模块
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------
//
//using System.Reflection;
//
//namespace TAF.DI
//{
//    using Core;
//    using Validation;
//
//    /// <summary>
//    /// Ioc配置模块
//    /// </summary>
//    public abstract class ConfigBase : Module
//    {
//        protected override void Load(ContainerBuilder builder)
//        {
//            base.Load(builder);
//            builder.RegisterType<Validator>().As<IValidator>();
//            builder.RegisterType<NothingValidationHandler>().As<IValidationHandler>();
//        }
//    }
//}