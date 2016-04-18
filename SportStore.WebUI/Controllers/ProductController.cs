using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entitites;
using SportStore.WebUI.Models;


namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repsitory;
        public int PageSize = 4;
        // GET: Product
        public ProductController(IProductRepository productRepository) 
        {
            this.repsitory = productRepository;
        }

        public ViewResult List(string category,int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repsitory.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repsitory.Products.Count() : repsitory.Products.Where(e => e.Category == category).Count()

                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}