﻿//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="IOC.cs" company="">
////   
//// </copyright>
//// <summary>
////   容器
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------
//
//namespace TAF
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Reflection;
//    using System.Text.RegularExpressions;
//
//    using Castle;
//
//    using TAF.DI;
//
//    using Container = DI.Container;
//
//    /// <summary>
//    /// 容器
//    /// </summary>
//    public static class Ioc
//    {
//        /// <summary>
//        /// 需要跳过的程序集列表
//        /// </summary>
//        private const string AssemblySkipLoadingPattern =
//            "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";
//
//        /// <summary>
//        /// 创建实例
//        /// </summary>
//        /// <typeparam name="T">
//        /// 实例类型
//        /// </typeparam>
//        /// <returns>
//        /// The <see cref="T"/>.
//        /// </returns>
//        public static T Create<T>() where T : class
//        {
//            return Container.Create<T>();
//        }
//
//        /// <summary>
//        /// 创建实例
//        /// </summary>
//        /// <param name="type">
//        /// 对象类型
//        /// </param>
//        /// <returns>
//        /// The <see cref="object"/>.
//        /// </returns>
//        public static object Create(Type type)
//        {
//            return Container.Create(type);
//        }
//
//        /// <summary>
//        /// 创建实例
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="type">
//        /// 对应映射接口的类型
//        /// </param>
//        /// <returns></returns>
//        public static T Create<T>(string type)
//        {
//            return Container.Create<T>(type);
//        }
//
//
//        /// <summary>
//        /// 注册程序集列表中所有实现了IDependency的类型
//        /// </summary>
//        /// <param name="assemblies">
//        /// The assemblies.
//        /// </param>
//        /// <param name="builder">
//        /// The builder.
//        /// </param>
//        private static void RegisterTypes(IEnumerable<Assembly> assemblies, ContainerBuilder builder)
//        {
//            var typeBase = typeof(IDependency);
//            builder.RegisterAssemblyTypes(FilterSystemAssembly(assemblies))
//                .Where(t => typeBase.IsAssignableFrom(t) && t != typeBase && !t.IsAbstract)
//                .AsImplementedInterfaces()
//                .InstancePerLifetimeScope();
//        }
//
//        /// <summary>
//        /// 过滤系统程序集
//        /// </summary>
//        /// <param name="assemblies">
//        /// The assemblies.
//        /// </param>
//        /// <returns>
//        /// The <see cref="Assembly[]"/>.
//        /// </returns>
//        private static Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
//        {
//            return
//                assemblies.Where(
//                                 assembly =>
//                                 !Regex.IsMatch(
//                                                assembly.FullName,
//                                     AssemblySkipLoadingPattern,
//                                     RegexOptions.IgnoreCase | RegexOptions.Compiled)).ToArray();
//        }
//
//        /// <summary>
//        /// 注册依赖
//        /// </summary>
//        /// <param name="modules">
//        /// 依赖配置
//        /// </param>
//        public static void Register(params IModule[] modules)
//        {
//            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
//            Container.Register(builder => RegisterTypes(assemblies, builder), modules);
//        }
//    }
//}