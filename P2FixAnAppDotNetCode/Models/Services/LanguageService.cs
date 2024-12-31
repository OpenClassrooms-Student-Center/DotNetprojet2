using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services method to manage the application language
    /// </summary>
    public class LanguageService : ILanguageService
    {
        /// <summary>
        /// Set the UI language
        /// </summary>
        public void ChangeUiLanguage(HttpContext context, string language)
        {
            string culture = SetCulture(language);
            UpdateCultureCookie(context, culture);
        }

        /// <summary>
        /// Set the culture
        /// </summary>

        public string SetCulture(string language)
        {
            string culture = "fr-FR";

            switch (language.ToLower())
            {
                case "english":
                    culture = "en-US";
                    break;
                case "french":
                    culture = "fr-FR";
                    break;
                case "spanish":
                    culture = "es-ES";
                    break;
                default:
                    culture = "fr-FR";
                    break;
            }

            return culture;
        }

        /// <summary>
        /// Update the culture cookie
        /// </summary>
        public void UpdateCultureCookie(HttpContext context, string culture)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
        }
    }
}
