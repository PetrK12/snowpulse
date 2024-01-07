using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObject;

public class RegisterDto
{
    [Required]
    public string Displayname { get; set; }
    [Required]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_" +
                       "+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must contain at least" +
        " 1 uppercase, 1 lowercase, 1 number, 1 special character")]
    public string Password { get; set; }
    [Required]
    [EmailAddress] 
    public string Email { get; set; }
}