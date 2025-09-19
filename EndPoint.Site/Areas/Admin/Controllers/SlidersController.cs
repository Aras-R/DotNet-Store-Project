using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.commands.EditSlider;
using Store.Application.Services.HomePages.commands.RemoveSlider;
using Store.Application.Services.HomePages.Commands.AddNewSlider;
using Store.Application.Services.HomePages.Queries.GetSliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
   [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IAddNewSliderService _addNewSliderService;
        private readonly IGetSlidersService _getSlidersService;
        private readonly IRemoveSliderService _removeSliderService;
        private readonly IEditSliderService _editSliderService;
        public SlidersController(
            IAddNewSliderService addNewSliderService,
            IGetSlidersService getSlidersService,
            IRemoveSliderService removeSliderService,
            IEditSliderService editSliderService)
        {
            _addNewSliderService = addNewSliderService;
            _getSlidersService = getSlidersService;
            _removeSliderService = removeSliderService;
            _editSliderService = editSliderService;
        }

        public IActionResult Index()
        {
            var result = _getSlidersService.Execute();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link)
        {
            var result = _addNewSliderService.Execute(file, link);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long Id)
        {
            var result = _removeSliderService.Execute(Id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Edit(long SliderId, string Link)
        {
            var result = _editSliderService.Execute(new RequestEditSliderDto
            {
                SliderId = SliderId,
                Link = Link
            });

            return Json(result);
        }

    }
}
