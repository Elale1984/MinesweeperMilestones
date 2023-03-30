using CST_350_Minesweeper.Models;
using CST_350_Minesweeper.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Milestone.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> ProcessLogin(User user)
    {
        LoginService loginService = new LoginService();

        if (loginService.IsValid(user) != null)
        {
            User currentUser = loginService.GetCurrentLoggedInUser();

            // Create a list of claims for the authenticated user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, currentUser.Username),
                // Add any additional claims for the user, such as a role claim
            };

            // Create an identity for the user with the claims
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create a principal for the user with the identity
            var principal = new ClaimsPrincipal(identity);

            // Sign in the user by creating an authentication cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirect to the game index action
            return RedirectToAction("Index", "Game");
        }
        else
        {
            return View("LoginFailure", user);
        }
    }
}
