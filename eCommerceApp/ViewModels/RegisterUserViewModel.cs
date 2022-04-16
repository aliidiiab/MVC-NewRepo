using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ViewModels
{
    public class RegisterUserViewModel
    {
       
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password didn't match!")]
        public string ConfirmPassword { get; set; }

    }
}
