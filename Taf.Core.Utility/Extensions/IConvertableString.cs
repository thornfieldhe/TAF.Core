// --------------------------------------------------------------------------------------------------------------------
// <copyright file="$CLASS$.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

// 何翔华
// Taf.Core.Utility
// IConvertableString.cs

namespace Taf.Core.Utility;



public interface IExtension<V>{
    V GetValue();
}

public static class ExtensionGroup{
    private static Dictionary<Type, Type> cache = new ();

    public static T As<T>(this string   v) where T : IExtension<string>   => As<T, string>(v);
    public static T As<T>(this double   v) where T : IExtension<double>   => As<T, double>(v);
    public static T As<T>(this decimal  v) where T : IExtension<decimal>  => As<T, decimal>(v);
    public static T As<T>(this DateTime v) where T : IExtension<DateTime> => As<T, DateTime>(v);

    private static T As<T, V>(this V v) where T : IExtension<V>{
        Type t;
        var  valueType = typeof(T);
        if(cache.ContainsKey(valueType)){
            t = cache[valueType];
        } else{
            t = CreateType<T, V>();
            cache.Add(valueType, t);
        }

        var result = Activator.CreateInstance(t, v);
        return (T)result;
    }

    // 通过反射发出动态实现接口T
    private static Type CreateType<T, V>() where T : IExtension<V>{
        var targetInterfaceType = typeof(T);
        var generatedClassName  = targetInterfaceType.Name.Remove(0, 1);
        //
        var aName = new AssemblyName("ExtensionDynamicAssembly");
        var ab =
            AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
        var mb = ab.DefineDynamicModule(aName.Name);
        var tb = mb.DefineType(generatedClassName, TypeAttributes.Public);
        //实现接口
        tb.AddInterfaceImplementation(typeof(T));
        //value字段
        var valueFiled = tb.DefineField("value", typeof(V), FieldAttributes.Private);
        //构造函数
        var ctor = tb.DefineConstructor(MethodAttributes.Public,
                                        CallingConventions.Standard, new[]{ typeof(V) });
        var ctor1IL = ctor.GetILGenerator();
        ctor1IL.Emit(OpCodes.Ldarg_0);
        ctor1IL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
        ctor1IL.Emit(OpCodes.Ldarg_0);
        ctor1IL.Emit(OpCodes.Ldarg_1);
        ctor1IL.Emit(OpCodes.Stfld, valueFiled);
        ctor1IL.Emit(OpCodes.Ret);
        //GetValue方法
        var getValueMethod = tb.DefineMethod("GetValue",
                                             MethodAttributes.Public | MethodAttributes.Virtual, typeof(V)
                                           , Type.EmptyTypes);
        var numberGetIL = getValueMethod.GetILGenerator();
        numberGetIL.Emit(OpCodes.Ldarg_0);
        numberGetIL.Emit(OpCodes.Ldfld, valueFiled);
        numberGetIL.Emit(OpCodes.Ret);
        //接口实现
        var getValueInfo = targetInterfaceType.GetInterfaces()[0].GetMethod("GetValue");
        tb.DefineMethodOverride(getValueMethod, getValueInfo);
        //
        var t = tb.CreateType();
        return t;
    }
}
