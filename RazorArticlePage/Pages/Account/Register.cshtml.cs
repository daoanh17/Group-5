using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorArticlePage.Models;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RazorArticlePage.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        [BindProperty] public RegisterInputModel Input { get; set; }
        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<RegisterModel> logger)
        {
            (_userManager, _signInManager) = (userManager, signInManager);
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Email:" + Input.Email +  "Password:" + Input.Password);

            var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
            _logger.LogInformation("AppUser:" + user.Email);
            var result = await _userManager.CreateAsync(user, Input.Password);
            //_logger.LogInformation("Result:" + result.Errors.First<IdentityError>().Code);

            ModelState.AddModelError(string.Empty, result.Succeeded.ToString());
            if (result.Succeeded)
            {
                _logger.LogInformation($"Successfully registered user: {Input.Email}");
                await _signInManager.SignInAsync(user, false);
                return RedirectToPage("/Index");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return Page();
        }
        public class RegisterInputModel
        {
            [Required] public string Email { get; set; }
            [Required][DataType(DataType.Password)] public string Password { get; set; }
        }
    }

}
