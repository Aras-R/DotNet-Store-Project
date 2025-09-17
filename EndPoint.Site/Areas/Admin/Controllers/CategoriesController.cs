using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.commands.EditCategory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoriesController : Controller
    {

        private readonly IProductFacad _productFacad;

        public CategoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }


        public IActionResult Index(long? parentId)
        {
            return View(_productFacad.GetCategoriesService.Execute(parentId).Data);
        }

        [HttpGet]
        public IActionResult AddNewCategory(long? parentId)
        {
            ViewBag.parentId = parentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? ParentId, string Name)
        {
            var result = _productFacad.AddNewCategoryService.Execute(ParentId, Name);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long CategoryId)
        {
            var result = _productFacad.RemoveCategoryService.Execute(CategoryId);
            return Json(result);
        }
        [HttpPost]
        public IActionResult Edit(long CategoryId, string Name)
        {
            var result = _productFacad.EditCategoryService.Execute(new RequestEditCategoryDto
            {
                CategoryId = CategoryId,
                Name = Name
            });

            return Json(result);
        }


    }
}
