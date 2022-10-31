using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;

namespace WebMvc.Controllers;

public class AccountController:Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginDto() { ReturnUrl = returnUrl });
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        if (ModelState.IsValid)
        {
            if (login.Username == "alijon" && login.Password == "123")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Email, "ali@gmail.com")
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, "UserIdentity");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                
                //save cookie   
                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });
                
                if (string.IsNullOrEmpty(login.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home", new {area = "Admin"});
                }
                else
                {
                    return Redirect(login.ReturnUrl);
                }
            }
        }

        return View(login);
    }
    
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
}