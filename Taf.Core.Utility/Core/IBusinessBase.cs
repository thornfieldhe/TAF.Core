﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBusinessBase.cs" company="">
//   
// </copyright>
// <summary>
//   确保是业务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf;

namespace Taf.Core
{
    using System;


    /// <summary>
    /// 确保是业务类
    /// </summary>
    public interface IBusinessBase : IEntityBase, IBaseStatus
    {

        DateTime CreatedDate
        {
            get;
        }

        DateTime ChangedDate
        {
            get;
        }

        string Note
        {
            get;
        }

        int Status
        {
            get; set;
        }

        Guid CreatedBy
        {
            get; set;
        }

        Guid ModifyBy
        {
            get; set;
        }

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
        Guid Id
        {
            get;
        }
    }

}