using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password,
                false, false);

            if (result.Succeeded)
            {
                return Redirect(input.ReturnUrl);
            }

            return View(input);
        }


        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var user = new IdentityUser(input.Username);
            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(input.ReturnUrl);
            }

            return View();
        }
    }

    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string ReturnUrl { get; set; }
    }
}