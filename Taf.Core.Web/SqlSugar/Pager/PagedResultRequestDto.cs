// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagedResultRequestDto.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

// 何翔华
// Taf.Core.Net.Utility
// PagedResultRequestDto.cs

namespace Taf.Core.Web;

/// <summary>
/// 分页查询对象
/// </summary>
public record PagedResultRequestDto: LimitedResultRequestDto, IPagedResultRequest
{
    [Range(1, int.MaxValue)]
    public virtual int PageIndex{ get; set; }
}