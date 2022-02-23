using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HomeWork.Areas.AdminPanel.Data;
using HomeWork.DataLayerAccess;
using HomeWork.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ExpertImagesController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;


        public ExpertImagesController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var expertImages = await _dbContext.ExpertImages.ToListAsync();
            
            return View(expertImages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpertImages expertImages)
        {
            if (!ModelState.IsValid)
                return View();

            if (!expertImages.Photo.IsImage())
            {
                ModelState.AddModelError("Photo",   "Yuklediyinilen shekil olmalidir");
                return View();
            } 
            
            if (expertImages.Photo.IsAllowedSize(1))
            {
                ModelState.AddModelError("Photo",   "Yuklenilen shekil 1 mb-dan az olmalidir");
                return View();
            }
            
            var fileName = await expertImages.Photo.GenerateFile(Constants.ImageFolderPath);

            expertImages.Image = fileName;
            await _dbContext.ExpertImages.AddAsync(expertImages);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var expertImage = await _dbContext.ExpertImages.FirstOrDefaultAsync(x => x.Id == id);
            if (expertImage == null)
                return NotFound();

            return View(expertImage);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ExpertImages expertImages)
        {
            if (id == null)
                return NotFound();

            if (id != expertImages.Id)
                return BadRequest();

            var existExpertImage = await _dbContext.ExpertImages.FindAsync(id);
            if (existExpertImage == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(existExpertImage);

            if (!expertImages.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Yuklediyiniz shekil olmalidir.");
                return View(existExpertImage);
            }

            if (!expertImages.Photo.IsAllowedSize(1))
            {
                ModelState.AddModelError("Photo", "1 mb-dan az olmalidir.");
                return View(existExpertImage);
            }

            var path = Path.Combine(Constants.ImageFolderPath, existExpertImage.Name);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var fileName = await expertImages.Photo.GenerateFile(Constants.ImageFolderPath);
            existExpertImage.Name = fileName;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var expertImage = await _dbContext.ExpertImages.FirstOrDefaultAsync(x => x.Id == id);
            if (expertImage == null)
                return NotFound();

            return View(expertImage);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
                return NotFound();

            var existExpertImage = await _dbContext.ExpertImages.FindAsync(id);
            if (existExpertImage == null)
                return NotFound();

            var path = Path.Combine(Constants.ImageFolderPath, existExpertImage.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _dbContext.ExpertImages.Remove(existExpertImage);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            
            var expertImage = await _dbContext.ExpertImages.FindAsync(id);
            if (expertImage == null)
                return NotFound();

            return View(expertImage); 
        }
    }
}