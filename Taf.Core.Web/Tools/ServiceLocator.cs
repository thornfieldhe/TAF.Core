// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Core.Utility;

// 何翔华
// Taf.Core.Web
// ServiceLocator.cs

namespace Taf.Core.Web;

public  class ServiceLocator:SingletonBase<ServiceLocator>
{
    public  IServiceProvider ServiceProvider{ get; set; }
}

