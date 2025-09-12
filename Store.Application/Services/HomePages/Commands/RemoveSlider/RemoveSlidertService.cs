using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePages.commands.RemoveSlider
{
    public class RemoveSliderService : IRemoveSliderService
    {
        private readonly IDataBaseContext _context;

        public RemoveSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long SliderId)
        {
            var slider = _context.Sliders.Find(SliderId);
            if (slider == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اسلایدر یافت نشد"
                };
            }

            slider.IsRemoved = true;
            slider.RemoveTime = DateTime.Now;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "اسلایدر با موفقیت حذف شد"
            };
        }
    }
}


