using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Commands.AddHomePageImages;
using Store.Application.Services.HomePages.Queries.GetImagesAdmin;
using Store.Domain.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        private readonly IAddHomePageImagesService _addHomePageImagesService;
        private readonly IGetImagesAdminService _getImagesAdminService;
        public HomePageImagesController(
            IAddHomePageImagesService addHomePageImagesService,
            IGetImagesAdminService getImagesAdminService)
        {
            _addHomePageImagesService = addHomePageImagesService;
            _getImagesAdminService = getImagesAdminService;
        }
        public IActionResult Index()
        {
            var result = _getImagesAdminService.Execute();
            return View(result.Data);
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link , ImageLocation imageLocation)
        {
            _addHomePageImagesService.Execute(new requestAddHomePageImagesDto
            {
                file = file,
                ImageLocation = imageLocation,
                Link = link,
            });
            return View();
        }

    }
}
