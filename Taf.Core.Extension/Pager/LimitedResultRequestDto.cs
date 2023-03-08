// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LimitedResultRequestDto.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//  简单查询对象 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

// 何翔华
// Taf.Core.Net.Utility
// LimitedResultRequestDto.cs

namespace Taf.Core.Extension;

/// <summary>
/// 简单查询对象
/// </summary>
[Serializable]
public record LimitedResultRequestDto : ILimitedResultRequest, IValidatableObject
{
    /// <summary>
    /// Default value: 10.
    /// </summary>
    public static int DefaultMaxResultCount{ get; set; } = 20;

    /// <summary>
    /// Maximum possible value of the <see cref="PageSize"/>.
    /// Default value: 1,000.
    /// </summary>
    public static int MaxMaxResultCount{ get; set; } = 1000;

    /// <summary>
    /// Maximum result count should be returned.
    /// This is generally used to limit result count on paging.
    /// </summary>
    [Range(1, int.MaxValue)]
    public virtual int PageSize{ get; set; } = DefaultMaxResultCount;

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PageSize > MaxMaxResultCount)
        {
            yield return new ValidationResult("page size should less than max result count");
        }
    }
}