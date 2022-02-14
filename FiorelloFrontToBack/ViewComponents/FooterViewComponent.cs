using System.Threading.Tasks;
using HomeWork.DataLayerAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        
        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bio = await _dbContext.Bios.SingleOrDefaultAsync();

            return View(bio);
        }
    }
}