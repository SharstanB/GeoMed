using System.ComponentModel.DataAnnotations;

namespace GeoMed.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "MessageRequired")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "MessageRequired")]
        [StringLength(255, 
            ErrorMessageResourceName = "PasswordLength",
            MinimumLength = 6) ]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
