using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Full name Last Name before First Name")]
    public string FullName { get; set; } = default!;
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = default!;

    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; } = default!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = default!;
}
