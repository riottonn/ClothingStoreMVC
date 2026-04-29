using ClothingStore.Domain;
using ClothingStoreMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreMVC.Controllers
{
    public class AccountController : Controller
    {
        // ВИПРАВЛЕНО: Використовуємо <User> замість <IdentityUser>
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // ВИПРАВЛЕНО: У конструкторі тепер обидва менеджери працюють з класом <User>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ВИПРАВЛЕНО: Створюємо екземпляр User
                User user = new User { Email = model.Email, UserName = model.Email };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // ВИПРАВЛЕНО: SignInAsync тепер приймає об'єкт User
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Тепер цей метод працюватиме коректно, бо _signInManager ініціалізований правильно
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Неправильний логін чи пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}