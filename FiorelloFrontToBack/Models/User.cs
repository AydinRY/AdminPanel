using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HomeWork.Models
{
    public class User:IdentityUser
    {
        [Required]
        public string Fullname { get; set; }
    }
}