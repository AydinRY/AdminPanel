using HomeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.DataLayerAccess
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        
        public DbSet<SliderImage> SliderImages { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<About> Abouts { get; set; }

        public DbSet<Experts> Experts { get; set; }

        public DbSet<ExpertImages> ExpertImages { get; set; }

        public DbSet<Subscribe> Subscribes { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<BlogImages> BlogImages { get; set; }

        public DbSet<Say> Says { get; set; }

        public DbSet<Instagram> Instagrams { get; set; }

        public DbSet<Bio> Bios { get; set; }

    }
}