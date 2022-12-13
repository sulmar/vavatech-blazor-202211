using System.ComponentModel.DataAnnotations;

namespace Vavatech.AuthApi.Models;

public class LoginViewModel
{
    [Required]
    public string Login { get; set; }

    [Required, StringLength(20, MinimumLength = 3 )]
    public string Password { get; set; }
}
