using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = default!;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}