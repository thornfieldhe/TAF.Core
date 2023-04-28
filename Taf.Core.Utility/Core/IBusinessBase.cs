// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBusinessBase.cs" company="">
//   
// </copyright>
// <summary>
//   确保是业务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Taf.Core
{
    /// <summary>
    /// 确保是业务类
    /// </summary>
    public interface IBusinessBase : IEntityBase, IBaseStatus
    {

        /// <summary>
        /// 
        /// </summary>
        DateTime CreatedDate
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        DateTime ChangedDate
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        string Note
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        int Status
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        Guid CreatedBy
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        Guid ModifyBy
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        byte[] Version
        {
            get;
        }
    }

    /// <summary>
    /// 实体基类
    /// </summary>
    public interface IEntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        Guid Id
        {
            get;
        }
    }

}