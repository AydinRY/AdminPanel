using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models
{
    public class BlogImages
    {
        public int Id { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Bosh ola bilmez!"), MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Max 100 simvol ola biler")]
        public string Subtitle { get; set; }
    }
}