using System.Text;

namespace WebMvc.Models;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? ReturnUrl { get; set; }
}