// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebConst.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// 何翔华
// Taf.Core.Web
// WebConst.cs

namespace Taf.Core.Web;

/// <summary>
/// $Summary$
/// </summary>
public static class WebConst{
#region HTML 状态码

    public static int CodeOK           => 200;
    public static int CodeUnauthorized => 401;
    public static int CodeBadRequest   => 400;

    /// <summary>
    /// forbidden
    /// </summary>
    public static int CodeForbidden => 403;

    /// <summary>
    /// not  found
    /// </summary>
    public static int CodeNotFound => 404;

    /// <summary>
    /// server error
    /// </summary>
    public static int CodeInternalServerError => 500;

    /// <summary>
    /// service refused
    /// </summary>
    public static int CodeServiceUnavailable => 503;


#endregion
}
