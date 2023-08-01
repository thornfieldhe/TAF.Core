// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Int.cs" company="">
//   
// </copyright>
// <summary>
//   Int扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Taf.Core.Utility
{
    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 间隔秒转分钟
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public static double SecondsToMinutes(this long obj) => obj / 60D;
        
        /// <summary>
        /// 间隔分钟秒转秒
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public static long MinutesToSeconds(this long obj) => obj * 60;
        /// <summary>
        /// 间隔秒转小时
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public static double SecondsToHours(this long obj) => obj /3600D;
        
        /// <summary>
        /// 间隔秒转天
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public static double SecondsToDays(this long obj) => obj / 86400D;
    }
}