using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorArticlePage.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorArticlePage.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        [BindProperty] public LoginInputModel Input { get; set; }
        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger = null, UserManager<ApplicationUser> userManager = null)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelSate is invalid");
                return Page();
            }

            var inputUser = _userManager.NormalizeName(Input.Email);
            _logger.LogInformation(Input.Email + "-" + inputUser + Input.Password);

            var user = await _userManager.FindByNameAsync(Input.Email);
            if (user == null)
            {
                _logger.LogInformation($"{inputUser} not found");
                return Page();
            }

            _logger.LogInformation($"get user:" + user.NormalizedEmail + "-" + user.UserName);



            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, Input.Password);
            _logger.LogInformation($"Password valid: {isPasswordValid}");



            var result = await _signInManager.PasswordSignInAsync(inputUser, Input.Password, false, false);
            _logger.LogInformation($"Login successfully: {result}");
            if (result.Succeeded) return RedirectToPage("/Account/Index");
            //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
        public class LoginInputModel
        {
            [Required] public string Email { get; set; }
            [Required][DataType(DataType.Password)] public string Password { get; set; }
        }

    }
}
