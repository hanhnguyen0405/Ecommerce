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
            ViewData["ProductCategories"] = GetProductCategoryVM();

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
        public IActionResult ViewProduct(int Id)
        {
            Product product = new Product();
            product = _bllProduct.GetProductById(Id);
            ProductVM productVM = _mapper.Map<ProductVM>(product);
            return View(productVM);
        }

        //Edit a product
        [HttpGet]
        public IActionResult EditProduct(int Id)
        {
            Product product = new Product();
            product = _bllProduct.GetProductById(Id);
            ProductVM productVM = _mapper.Map<ProductVM>(product);

            ViewData["ProductCategories"] = GetProductCategoryVM();

            return View(productVM);
        }

        //save changes in product
        [HttpPost]
        public IActionResult EditProduct(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productVM);
                _bllProduct.UpdateProduct(product);

                return RedirectToAction("Index");
            }

            return View();
        }

        //Delete a product
        [HttpGet]
        public IActionResult DeleteProduct(int Id)
        {
            _bllProduct.DeleteProduct(Id);
            return RedirectToAction("Index");
        }

        //Add to Order
        [HttpGet]
        public IActionResult AddToOrder(ProductVM productVM)
        {
            OrderItem orderItem = new OrderItem()
            {
                ProductId = productVM.Id,
                Quantity = productVM.Quantity,
                UnitPrice = productVM.Price.UnitPrice
            };
            
            return View();
        }


        private List<ProductCategoryVM> GetProductCategoryVM()
        {
            List<ProductCategory> pcs = new List<ProductCategory>();
            pcs = _bllProduct.GetAllProductCategories().Cast<ProductCategory>().ToList();
            List<ProductCategoryVM> pcVMs = new List<ProductCategoryVM>();
            foreach (ProductCategory pc in pcs)
            {
                pcVMs.Add(_mapper.Map<ProductCategoryVM>(pc));
            }
            return pcVMs;
        }
    }
}