using System.ComponentModel.DataAnnotations;

namespace MVCproject.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
