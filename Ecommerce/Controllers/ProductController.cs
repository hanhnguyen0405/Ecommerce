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
using AutoMapper;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private IBLL_Product _bllProduct;
        IMapper _mapper;

        public ProductController(IConfiguration iconfiguration, IMapper mapper)
        {
            _bllProduct = new BLL_Product(iconfiguration);
            _mapper = mapper;
        }
        
        //List all products
        public IActionResult Index()
        {
            List<Product> products = _bllProduct.GetAllProducts().ToList();
            List<ProductVM> productVMs = new List<ProductVM>();
            foreach (Product product in products)
            {
                productVMs.Add(_mapper.Map<ProductVM>(product));
            }
            return View(productVMs);
        }

        //Create a product
        [HttpGet]
        public IActionResult CreateProduct()
        {
            //ProductVM productVM = new ProductVM();
            List<ProductCategory> pcs = new List<ProductCategory>();
            pcs = _bllProduct.GetAllProductCategories().Cast<ProductCategory>().ToList();
            List<ProductCategoryVM> pcVMs = new List<ProductCategoryVM>();
            foreach (ProductCategory pc in pcs)
            {
                pcVMs.Add(_mapper.Map<ProductCategoryVM>(pc));
            }
            ViewData["ProductCategories"] = pcVMs;
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productVM);
                _bllProduct.CreatNewProduct(product);

                return RedirectToAction("Index");
            }

            return View();
        }

        //view a product
        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            Product product = new Product();
            product = _bllProduct.GetProductById(productId);
            return View();
        }

    }
}