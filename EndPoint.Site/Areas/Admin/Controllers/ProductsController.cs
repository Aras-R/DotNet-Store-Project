using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.commands.EditProduct;
using Store.Application.Services.Products.commands.EditProductFeature;
using Store.Application.Services.Products.Commands.AddNewProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        private readonly IProductFacad _productFacad;

        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        //Index
        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            return View(_productFacad.GetProductForAdminService.Execute(Page, PageSize).Data);
        }

        //Detail
        public IActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailForAdminService.Execute(Id).Data);
        }

        //AddNewProduct
        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data,"Id" , "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Features = Features;
            return Json(_productFacad.AddNewProductService.Execute(request));
        }

        //Delete
        [HttpPost]
        public IActionResult Delete(long ProductId)
        {
            return Json(_productFacad.RemoveProductService.Execute(ProductId));
        }

        //Edit
        [HttpPost]
        public IActionResult Edit(long ProductId, string Name, string Brand, string Description, int Price, int Inventory, bool Displayed, long CategoryId)
        {
            var result = _productFacad.EditProductService.Execute(new RequestEditProductDto
            {
                ProductId = ProductId,
                Name = Name,
                Brand = Brand,
                Description = Description,
                Price = Price,
                Inventory = Inventory,
                Displayed = Displayed,
                CategoryId = CategoryId
            });

            return Json(result);
        }

        //Get Categories for edit of products
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _productFacad.GetAllCategoriesService.Execute().Data
                .Select(c => new { c.Id, c.Name })
                .ToList();

            return Json(categories);
        }

        //Edit Feature
        [HttpPost]
        public IActionResult EditFeature(long FeatureId, string DisplayName, string Value)
        {
            var result = _productFacad.EditProductFeatureService.Execute(new RequestEditProductFeatureDto
            {
                FeatureId = FeatureId,
                DisplayName = DisplayName,
                Value = Value
            });

            return Json(result);
        }

        //Delete Feature
        [HttpPost]
        public IActionResult DeleteFeature(long FeatureId)
        {
            return Json(_productFacad.RemoveProductFeatureService.Execute(FeatureId));
        }


    }
}
