using System.ComponentModel.DataAnnotations;

namespace ResourceManagement_Fe_App.Authentication
{
    public class LoginResult
    {
        public string message { get; set; }
        public string email { get; set; }
        public string jwtBearer { get; set; }
        public bool success { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string email { get; set; } // NOTE: email will be the username, too

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
    public class RegModel : LoginModel
    {
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password and confirm password do not match.")]
        public string confirmpwd { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [DataType(DataType.Text)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [DataType(DataType.Text)]
        public string lastname { get; set; }
    }

    public class UserData
    {

    }
}
