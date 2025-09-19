using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePages.commands.EditHomePageImage
{
    public interface IEditHomePageImageService
    {
        ResultDto Execute(RequestEditHomePageImageDto request);
    }

    public class EditHomePageImageService : IEditHomePageImageService
    {
        private readonly IDataBaseContext _context;

        public EditHomePageImageService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditHomePageImageDto request)
        {
            var image = _context.HomePageImages.Find(request.ImageId);
            if (image == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عکس یافت نشد"
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

            // چک اعتبار محل تصویر
            if (!Enum.IsDefined(typeof(ImageLocation), request.ImageLocation))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محل نمایش معتبر نیست"
                };
            }

            // اعمال تغییرات
            image.link = request.Link;
            image.ImageLocation = request.ImageLocation;
            image.UpdateTime = DateTime.Now;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویرایش عکس با موفقیت انجام شد"
            };
        }
    }

    public class RequestEditHomePageImageDto
    {
        public long ImageId { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
