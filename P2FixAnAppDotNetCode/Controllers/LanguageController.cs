using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models.Services;
using P2FixAnAppDotNetCode.Models.ViewModels;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeUiLanguage(LanguageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Language))
                {
                    _languageService.ChangeUiLanguage(HttpContext, model.Language);
                }

                string returnUrl = !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "/";
                return Redirect(returnUrl);
            }

            return View(model);
        }
    }
}
