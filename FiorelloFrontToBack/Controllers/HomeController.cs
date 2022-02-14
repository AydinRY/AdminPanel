using System.Linq;
using System.Threading.Tasks;
using HomeWork.DataLayerAccess;
using HomeWork.Models;
using HomeWork.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var sliderImages = await _dbContext.SliderImages.ToListAsync();
            var slider = await _dbContext.Sliders.SingleOrDefaultAsync();
            var categories = await _dbContext.Categories.ToListAsync();
            var products = await _dbContext.Products.Include(x => x.Category).ToListAsync();
            var about = await _dbContext.Abouts.SingleOrDefaultAsync();
            var experts = await _dbContext.Experts.SingleOrDefaultAsync();
            var expertImages = await _dbContext.ExpertImages.ToListAsync();
            var subscribe = await _dbContext.Subscribes.SingleOrDefaultAsync();
            var blogs = await _dbContext.Blogs.SingleOrDefaultAsync();
            var blogImages = await _dbContext.BlogImages.ToListAsync();
            var says = await _dbContext.Says.ToListAsync();
            var instagrams = await _dbContext.Instagrams.ToListAsync();
            
            return View(new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider,
                Categories = categories,
                Products = products,
                About = about,
                Experts = experts,
                ExpertImagesList = expertImages,
                Subscribes = subscribe,
                Blogs = blogs,
                BlogImages = blogImages,
                Says = says,
                Instagrams = instagrams,
            });
        }
    }
}