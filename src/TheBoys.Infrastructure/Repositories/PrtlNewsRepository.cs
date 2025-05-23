using TheBoys.Contracts.News;
using TheBoys.Shared.Extensions;
using static TheBoys.Contracts.News.NewsPaginationContarct;

namespace TheBoys.Infrastructure.Repositories;

public sealed class PrtlNewsRepository : Repository<PrtlNews>, IPrtlNewsRepository
{
    public PrtlNewsRepository(MnfPortalsDbContext context)
        : base(context) { }

    public async Task<NewsPaginationContarct> PaginateAsync(
        PaginateNewsContract contract,
        CancellationToken cancellationToken = default
    )
    {
        var resultContract = new NewsPaginationContarct();

        var query = _entities
            .AsNoTracking()
            .Include(x => x.PrtlNewsTranslations)
            .ThenInclude(x => x.Lang)
            .Where(x =>
                x.Published
                && x.PrtlNewsTranslations != null
                && x.PrtlNewsTranslations.Any(x => x.LangId == contract.LanguageId)
            )
            .Select(n => new
            {
                NewsId = n.NewsId,
                NewsDate = n.NewsDate,
                IsFeatured = n.IsFeatured,
                NewsImg = n.NewsImg,
                OwnerId = n.OwnerId,
                Translation = n.PrtlNewsTranslations.FirstOrDefault(x =>
                    x.LangId == contract.LanguageId
                )
            });

        if (contract.Search.HasValue())
        {
            query = query.Where(x =>
                EF.Functions.Like(x.Translation.NewsHead, $"{contract.Search}%")
            );
        }

        resultContract.TotalCount = await query.CountAsync(cancellationToken);
        resultContract.Elements = await query
            .OrderByDescending(x => x.NewsDate)
            .Paginate(contract.PageIndex, contract.PageSize)
            .Where(x => x.Translation != null)
            .Select(x => new NewsContract
            {
                Id = x.NewsId,
                Date = x.NewsDate,
                IsFeatured = x.IsFeatured,
                NewsImg = StringExtensions.GetFullPath(x.OwnerId.Value, x.NewsImg),
                NewsDetails =
                    x.Translation == null
                        ? null!
                        : new NewsTranslationContract
                        {
                            Id = x.Translation.Id,
                            LanguageId = x.Translation.LangId,
                            Head = StringExtensions.StripHtml(x.Translation.NewsHead),
                            Abbr = StringExtensions.StripHtml(x.Translation.NewsAbbr),
                            Body = StringExtensions.StripHtml(x.Translation.NewsBody),
                            Source = StringExtensions.StripHtml(x.Translation.NewsSource),
                            ImgAlt = x.Translation.ImgAlt
                        },
            })
            .ToListAsync(cancellationToken);

        return resultContract;
    }
}
