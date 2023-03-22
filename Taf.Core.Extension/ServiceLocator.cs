// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// ServiceLocator.cs

namespace Taf.Core.Extension;

using System;

public  class ServiceLocator:SingletonBase<ServiceLocator>
{
    public  IServiceProvider ServiceProvider{ get; set; }
}

