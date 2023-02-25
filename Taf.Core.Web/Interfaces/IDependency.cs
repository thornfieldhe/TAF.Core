// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IService.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Summary
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// 何翔华
// Taf.Core.Net.Utility
// IService.cs

namespace Taf.Core.Web;

using System;

/// <summary>
/// All classes implement this interface are automatically registered to dependency injection as transient object.
/// </summary>
public interface ITransientDependency
{

}
/// <summary>
/// All classes implement this interface are automatically registered to dependency injection as singleton object.
/// </summary>
public interface ISingletonDependency
{

}


