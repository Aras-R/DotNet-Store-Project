using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePages.commands.EditSlider
{
    public interface IEditSliderService
    {
        ResultDto Execute(RequestEditSliderDto request);
    }

    public class EditSliderService : IEditSliderService
    {
        private readonly IDataBaseContext _context;

        public EditSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditSliderDto request)
        {
            var slider = _context.Sliders.Find(request.SliderId);
            if (slider == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اسلایدر یافت نشد"
                };
            }

            // چک لینک
            if (string.IsNullOrWhiteSpace(request.Link))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "لینک نمی‌تواند خالی باشد"
                };
            }

            // اعمال تغییرات
            slider.link = request.Link;
            slider.UpdateTime = DateTime.Now;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویرایش اسلایدر با موفقیت انجام شد"
            };
        }
    }

    public class RequestEditSliderDto
    {
        public long SliderId { get; set; }
        public string Link { get; set; }
    }
}
