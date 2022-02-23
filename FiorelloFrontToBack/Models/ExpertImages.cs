using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace HomeWork.Models
{
    public class ExpertImages
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}