using System.Threading.Tasks;
using HomeWork.DataLayerAccess;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var blogImages = await _dbContext.BlogImages.ToListAsync();
            
            return View(blogImages);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            
            var blogImage = await _dbContext.BlogImages.FindAsync(id);
            if (blogImage == null)
                return NotFound();

            return View(blogImage); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogImages blogImages)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExistBlogImages = await _dbContext.BlogImages.AnyAsync(x => x.Title.ToLower() == blogImages.Title.ToLower());
            if (isExistBlogImages)
            {
                ModelState.AddModelError("Title", "Bu adda Blog var.");
                return View();
            }

            await _dbContext.BlogImages.AddAsync(blogImages);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}