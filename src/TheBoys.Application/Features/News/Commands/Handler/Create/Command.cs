namespace TheBoys.Application.Features.News.Commands.Handler.Create;

public sealed record CreateNewsCommand : IRequest<Response>
{
    public Guid? OwnerId { get; set; }
    public string NewsImg { get; set; }
    public bool Published { get; set; }
    public bool IsFeatured { get; set; }
    public List<CreateNewsTranslationCommand> Translations { get; set; } = new();

    public sealed record CreateNewsTranslationCommand
    {
        public string NewsHead { get; set; }

        public string NewsAbbr { get; set; }

        public string NewsBody { get; set; }

        public string NewsSource { get; set; }

        public int LangId { get; set; }

        public int NewsId { get; set; }

        public string ImgAlt { get; set; }
    }
}
