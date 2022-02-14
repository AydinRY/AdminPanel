using System.Linq;
using System.Threading.Tasks;
using HomeWork.DataLayerAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.Include(x => x.Category).Take(4).ToListAsync();
            
            return View(products);
        }

        public async Task<IActionResult> Load(int skip)
        {
            var products = await _dbContext.Products.Include(x => x.Category).Skip(skip).Take(4).ToListAsync();

            return PartialView("_ProductPartial", products);
        }
    }
}