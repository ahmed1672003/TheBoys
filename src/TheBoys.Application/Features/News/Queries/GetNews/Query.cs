using MediatR;
using TheBoys.Application.Features.News.Dtos;
using TheBoys.Shared.Base.Responses;

namespace TheBoys.Application.Features.News.Queries.GetNews;

public sealed record GetNewsQuery(int Id, int LanguageId) : IRequest<ResponseOf<NewsDto>>;
