// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SendMailDto.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   提供模板名称和对象json化字符串
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Taf.Core.Web{
    /// <summary>
    /// 提供模板名称和对象json化字符串
    /// </summary>
    [Serializable]
    public record SendMailDto(object Entity = default, string To = "", string TemplateName = ""){
        /// <summary>
        /// 邮件对象json序列化字符串
        /// </summary>
        /// <param name="entity"></param>
        public string DataString => JsonConvert.SerializeObject(Entity);
    }
}
