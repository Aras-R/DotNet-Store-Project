using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Application.Interfaces.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Products.Queries.GetProductForSite;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;

        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(Ordering ordering, string Searchkey, long? CatId = null, int page = 1, int Pagesize=20)
        {
            return View(_productFacad.GetProductForSiteService.Execute(ordering,Searchkey, page, Pagesize, CatId).Data);
        }


        public IActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailForSiteService.Execute(Id).Data);
        }
    }
}
