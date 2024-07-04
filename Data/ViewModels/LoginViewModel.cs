using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp;

public class LoginViewModel
{
    [Display(Name = "Username")]
    public string Username { get; set; } = default!;
    
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}