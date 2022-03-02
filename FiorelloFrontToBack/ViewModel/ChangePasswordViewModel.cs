using System.ComponentModel.DataAnnotations;

namespace HomeWork.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string CurrentPassword { get; set; } 
    }
}