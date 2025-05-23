﻿namespace TheBoys.Shared.Base.Requests;

public record PaginateRequest
{
    public virtual int PageIndex { get; set; } = 1;
    public virtual int PageSize { get; set; } = 10;
    public string? Search { get; set; } = string.Empty;
}
