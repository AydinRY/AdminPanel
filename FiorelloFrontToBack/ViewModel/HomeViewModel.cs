using System.Collections.Generic;

namespace HomeWork.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; }

        public Slider Slider { get; set; }

        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public About About { get; set; }

        public List<ExpertImages> ExpertImagesList { get; set; }

        public Experts Experts { get; set; }
        public Subscribe Subscribes { get; set; }
        public Blog Blogs { get; set; }
        public List<BlogImages> BlogImages { get; set; }

        public List<Say> Says { get; set; }

        public List<Instagram> Instagrams { get; set; }
    }
}