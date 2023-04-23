// 何翔华
// Taf.Core.Net.Utility
// PagedAndSortedResultRequestDto.cs

namespace Taf.Core.Web;

/// <summary>
/// 默认分页排序查询接口
/// </summary>
[Serializable]
public record PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
{
    public virtual string? Sorting{ get; set; }
    public         bool?    Asc    { get; set; }
}

/// <summary>
///     默认查询对象
/// </summary>
public record BaseQueryRequestDto : PagedAndSortedResultRequestDto{
    private string _keyWord;

    public BaseQueryRequestDto() => PageSize = 20;

    public virtual string? KeyWord{
        get => _keyWord;
        set => _keyWord = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}