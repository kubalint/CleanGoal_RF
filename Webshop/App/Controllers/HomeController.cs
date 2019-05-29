using App.Mappers;
using App.Models.ViewModels.Product;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            StoreContext db = new StoreContext();

            var products = db.Products.Where(x=>x.PhotoID!=null).ToList();
            var newestProducts = Enumerable.Reverse(products).Take(4).ToList();

            ProductsViewModel productsViewModel = new ProductsViewModel();

            foreach (var product in newestProducts)
            {
                ProductViewModel pvm = CustomerProductMappers.ProductToViewModel(product);
                pvm.Photo = CustomerProductMappers.PhotoToViewModel(product.Photo);
                productsViewModel.ProductList.Add(pvm);
            }

            return View(productsViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}