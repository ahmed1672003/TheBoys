namespace TheBoys.Application.Features.News.Queries.GetNewsDetails;

public sealed record GetNewsDetailsQuery(int Id) : IRequest<ResponseOf<GetNewsDetailsResult>>;
