using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBoys.API.Data;

namespace TheBoys.API.Controllers.News
{
    [Route("api/v1/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context) => _context = context;

        [HttpGet()]
        public async Task<IActionResult> PaginateNewsAsync(
            CancellationToken cancellationToken = default
        )
        {
            var news = await _context.News.AsNoTracking().ToListAsync(cancellationToken);
            return Ok(news);
        }
    }
}
