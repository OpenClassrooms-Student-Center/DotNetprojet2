using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;
using System;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;
        private readonly IMemoryCache _memoryCache;

        public ProductController(IProductService productService, ILanguageService languageService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _languageService = languageService;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        /* public IActionResult Index()
         {
             // Définir la clé de cache pour les produits
             string cacheKey = "productList";

             // Tenter de récupérer les produits depuis le cache
             if (!_memoryCache.TryGetValue(cacheKey, out var products))
             {
                 // Si les produits ne sont pas en cache, les récupérer depuis le service
                 products = _productService.GetAllProducts();

                 // Configurer les options de cache (optionnel)
                 var cacheOptions = new MemoryCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromMinutes(60))  // Expiration après 60 minutes d'inactivité
                     .SetAbsoluteExpiration(TimeSpan.FromHours(2));   // Expiration après 2 heures

                 // Mettre les produits en cache
                 _memoryCache.Set(cacheKey, products, cacheOptions);
             }

             return View(products);
         }*/

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}