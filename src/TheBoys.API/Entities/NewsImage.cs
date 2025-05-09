using System.ComponentModel.DataAnnotations.Schema;

namespace TheBoys.API.Entities;

public class NewsImage
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("NewsId")]
    public int NewsId { get; set; }

    [Column("NewsUrl")]
    public string NewsUrl { get; set; }

    public News News { get; set; }
}
