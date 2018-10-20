using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLLEcommerce;
using BLLEcommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.Extensions.Configuration;
using CommonModels;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private IBLL_Product _bllProduct;

        public ProductController(IConfiguration iconfiguration)
        {
            _bllProduct = new BLL_Product(iconfiguration);
        }
        
        public IActionResult Index()
        {
            List<Product> products = _bllProduct.GetAllProducts().ToList();
            return View(products);
        }

        //Create a product
        [HttpGet]
        public IActionResult CreateProduct()
        {
            //string connectionString = _iconfiguration.GetConnectionString("Ecommerce_DefaultConnectionString");
            //ProductVM productVM = new ProductVM();
            ViewData["ProductCategories"] = _bllProduct.GetAllProductCategories().ToList();
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _bllProduct.CreatNewProduct(product);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}