// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumTest.cs" company="">
//   
// </copyright>
// <summary>
//   测试枚举
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;
using Taf.Test;
using Xunit;

namespace Taf.Core.Test
{
    using System;

    /// <summary>
    /// 测试枚举
    /// </summary>
    
    public class EnumTest
    {
        #region 常量

        /// <summary>
        /// LogType.Debug实例
        /// </summary>
        public const LogType DEBUG_INSTANCE = LogType.Debug;

        /// <summary>
        /// LogType.Debug的名称：Debug
        /// </summary>
        public const string DEBUG_NAME = "Debug";

        /// <summary>
        /// LogType.Debug的值：5
        /// </summary>
        public const int DEBUG_VALUE = 5;

        /// <summary>
        /// LogType.Debug的描述："调试"
        /// </summary>
        public const string DEBUG_DESCRIPTION = "调试";

        #endregion

        #region GetInstance(获取实例)

        /// <summary>
        /// 1. 功能：获取实例,
        /// 2. 场景：参数为null
        /// 3. 预期：抛出异常
        /// </summary>
        [Fact]
        public void TestGetInstance_ArgumentIsNull_Throw()
        {

                Assert.Throws<ArgumentNullException>(()=>EnumExt.GetInstance<LogType>(null));
        }

        /// <summary>
        /// 1. 功能：获取实例,
        /// 2. 场景：参数为空字符串
        /// 3. 预期：抛出异常
        /// </summary>
        [Fact]
        public void TestGetInstance_ArgumentIsEmpty_Throw()
        {
            Assert.Throws<ArgumentNullException>(()=>EnumExt.GetInstance<LogType>(string.Empty));
        }

        /// <summary>
        /// 通过成员名获取实例
        /// </summary>
        [Fact]
        public void GetInstance_Name()
        {
            Assert.Equal(DEBUG_INSTANCE, EnumExt.GetInstance<LogType>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取实例
        /// </summary>
        [Fact]
        public void GetInstance_Value()
        {
            Assert.Equal(DEBUG_INSTANCE, EnumExt.GetInstance<LogType>(DEBUG_VALUE));
        }

        /// <summary>
        /// 通过成员名获取可空枚举实例
        /// </summary>
        [Fact]
        public void GetInstance_Name_Nullable()
        {
            Assert.Equal(DEBUG_INSTANCE, EnumExt.GetInstance<LogType?>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取可空枚举实例
        /// </summary>
        [Fact]
        public void GetInstance_Value_Nullable()
        {
            Assert.Equal(DEBUG_INSTANCE, EnumExt.GetInstance<LogType?>(DEBUG_VALUE));
        }

        #endregion

        #region GetName(获取成员名)

        /// <summary>
        /// 1. 功能：获取成员名,
        /// 2. 场景：参数为空，
        /// 3. 预期：返回空字符串
        /// </summary>
        [Fact]
        public void GetName_ArgumentIsNull_ReturnEmpty()
        {
            Assert.Equal(string.Empty, EnumExt.GetName<LogType>(null));
        }

        /// <summary>
        /// 通过成员名获取成员名
        /// </summary>
        [Fact]
        public void GetName_Name()
        {
            Assert.Equal(DEBUG_NAME, EnumExt.GetName<LogType>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取成员名
        /// </summary>
        [Fact]
        public void GetName_Value()
        {
            Assert.Equal(DEBUG_NAME, EnumExt.GetName<LogType>(DEBUG_VALUE));
        }

        /// <summary>
        /// 通过实例获取成员名
        /// </summary>
        [Fact]
        public void GetName_Instance()
        {
            Assert.Equal(DEBUG_NAME, EnumExt.GetName<LogType>(DEBUG_INSTANCE));
        }

        /// <summary>
        /// 通过成员值获取可空枚举成员名
        /// </summary>
        [Fact]
        public void GetName_Value_Nullable()
        {
            Assert.Equal(DEBUG_NAME, EnumExt.GetName<LogType?>(DEBUG_VALUE));
        }

        #endregion

        #region GetValue(获取成员值)

        /// <summary>
        /// 1. 功能：获取成员值,
        /// 2. 场景：参数为null
        /// 3. 预期：抛出异常
        /// </summary>
        [Fact]
        public void GetValue_ArgumentIsNull_Throw()
        {
            Assert.Throws<ArgumentNullException>(()=>EnumExt.GetValue<LogType>(null));
        }

        /// <summary>
        /// 1. 功能：获取成员值,
        /// 2. 场景：参数为空字符串，
        /// 3. 预期：抛出异常
        /// </summary>
        [Fact]
        public void GetValue_ArgumentIsEmpty_Throw()
        {
            Assert.Throws<ArgumentNullException>(()=>EnumExt.GetValue<LogType>(string.Empty));
        }

        /// <summary>
        /// 通过成员名获取成员值
        /// </summary>
        [Fact]
        public void GetValue_Name()
        {
            var actual = EnumExt.GetValue<LogType>(DEBUG_NAME);
            Assert.Equal(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过成员值获取成员值
        /// </summary>
        [Fact]
        public void GetValue_Value()
        {
            var actual = EnumExt.GetValue<LogType>(DEBUG_VALUE);
            Assert.Equal(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过实例获取成员值
        /// </summary>
        [Fact]
        public void GetValue_Instance()
        {
            var actual = EnumExt.GetValue<LogType>(DEBUG_INSTANCE);
            Assert.Equal(DEBUG_VALUE, actual);
            Assert.Equal(DEBUG_VALUE, DEBUG_INSTANCE.Value());
            Assert.Equal(DEBUG_VALUE, DEBUG_INSTANCE.Value<int>());
            Assert.Equal(DEBUG_VALUE.ToString(), DEBUG_INSTANCE.Value<string>());
        }

        /// <summary>
        /// 通过成员名获取可空枚举成员值
        /// </summary>
        [Fact]
        public void GetValue_Name_Nullable()
        {
            var actual = EnumExt.GetValue<LogType?>(DEBUG_NAME);
            Assert.Equal(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过成员值获取可空枚举成员值
        /// </summary>
        [Fact]
        public void GetValue_Value_Nullable()
        {
            var actual = EnumExt.GetValue<LogType?>(DEBUG_VALUE);
            Assert.Equal(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过实例获取可空枚举成员值
        /// </summary>
        [Fact]
        public void GetValue_Instance_Nullable()
        {
            var actual = EnumExt.GetValue<LogType?>(DEBUG_INSTANCE);
            Assert.Equal(DEBUG_VALUE, actual);
        }

        #endregion

        #region GetMemberDescription(获取描述)

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：参数为空，
        /// 3. 预期：返回空字符串
        /// </summary>
        [Fact]
        public void GetDescription_ArgumentIsNull_ReturnEmpty()
        {
            Assert.Equal(string.Empty, EnumExt.GetDescription<LogType>(null));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：参数为空字符串，
        /// 3. 预期：返回空字符串
        /// </summary>
        [Fact]
        public void GetDescription_ArgumentIsEmpty_ReturnEmpty()
        {
            Assert.Equal(string.Empty, EnumExt.GetDescription<LogType>(string.Empty));
        }

        /// <summary>
        /// 通过成员名获取描述
        /// </summary>
        [Fact]
        public void GetDescription_Name()
        {
            Assert.Equal(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取描述
        /// </summary>
        [Fact]
        public void GetDescription_Value()
        {
            Assert.Equal(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_VALUE));
        }

        /// <summary>
        /// 通过实例获取描述
        /// </summary>
        [Fact]
        public void GetDescription_Instance()
        {
            Assert.Equal(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_INSTANCE));
            Assert.Equal(DEBUG_DESCRIPTION, DEBUG_INSTANCE.Description());
        }

        /// <summary>
        /// 通过成员名获取可空枚举描述
        /// </summary>
        [Fact]
        public void GetDescription_Name_Nullable()
        {
            Assert.Equal(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType?>(DEBUG_NAME));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效成员名，
        /// 3. 预期：返回空字符串
        /// </summary>
        [Fact]
        public void GetDescription_InvalidName_ReturnEmpty()
        {
            Assert.Equal(string.Empty, EnumExt.GetDescription<LogType>("Debug1"));
            Assert.Equal(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>("Debug"));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效成员值，
        /// 3. 预期：返回空字符串
        /// </summary>
        [Fact]
        public void GetDescription_InvalidValue_ReturnEmpty()
        {
            Assert.Equal(string.Empty, EnumExt.GetDescription<LogType>(100));
            Assert.Equal(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(5));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效枚举，
        /// 3. 预期：返回空字符串
        /// </summary>
        [Fact]
        public void GetDescription_InvalidEnum_ReturnEmpty()
        {
            Assert.Equal("致命错误", EnumExt.GetDescription<LogType>(LogType.Fatal));
            Assert.Equal("Fatal", EnumExt.GetName<LogType>(LogType.Fatal));
            Assert.Equal(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_INSTANCE));
        }

        #endregion

        #region GetItems(获取描述项集合)

        /// <summary>
        /// 获取描述项集合
        /// </summary>
        [Fact]
        public void GetItems_Success()
        {
            var items = EnumExt.GetItems<LogType>();
            Assert.Equal(5, items.Count);
            Assert.Equal("致命错误", items[0].Text);
            Assert.Equal("1", items[0].Value);
            Assert.Equal("错误", items[3].Text);
            Assert.Equal("2", items[3].Value);
            Assert.Equal("警告", items[4].Text);
            Assert.Equal("3", items[4].Value);
        }

        /// <summary>
        /// 获取描述项集合,绑定的不是枚举
        /// </summary>
        [Fact]
        public void GetItems_NotIsEnum()
        {
             Assert.Throws<InvalidOperationException>(()=>EnumExt.GetItems<User>());
        }
        #endregion
    }
}