using System.ComponentModel.DataAnnotations;

namespace Trava.Scripts.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "Passcode required.")]
    public string? Passcode { get; set; }
}