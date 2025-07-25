using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthController : Controller
{
    [HttpGet("login")]
    public IActionResult Login(string returnUrl = "https://localhost:4200/product")
    {
        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Auth0");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        var c = SignOut(new AuthenticationProperties
        {
            RedirectUri = "https://localhost:4200/login"
        },
        CookieAuthenticationDefaults.AuthenticationScheme,
        "Auth0");
        return c;
    }
}
