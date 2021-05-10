namespace WebApi.Controllers
{
    using BusinessLayer;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using static Common.Constants;

    [Route("[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IUsers users;

        public AuthController(ILogger<AuthController> logger, IUsers users)
        {
            this.logger = logger;
            this.users = users;
        }

        private bool ValidateLogin(string userName, string password)
        {
            // For this sample, all logins are successful.
            return users.Validate(userName, password) != null;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            // Normally Identity handles sign in, but you can do it directly
            if (ValidateLogin(userName, password))
            {
                var claims = new List<Claim>
                    {
                        new Claim(Auth.UserClaim, userName),
                        new Claim(Auth.RoleClaim, "Member")
                    };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", Auth.UserClaim, Auth.RoleClaim)));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }

            return AccessDenied(returnUrl);
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return StatusCode(401);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
