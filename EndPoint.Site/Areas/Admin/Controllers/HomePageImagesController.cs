﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Application.Services.HomePages.AddHomePageImages;
using Store.Domain.Entities.HomePages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        private readonly IAddHomePageImagesService _addHomePageImagesService;
        public HomePageImagesController(IAddHomePageImagesService addHomePageImagesService)
        {
            _addHomePageImagesService = addHomePageImagesService;
        }
        public IActionResult Index()
        {
            return View();
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
