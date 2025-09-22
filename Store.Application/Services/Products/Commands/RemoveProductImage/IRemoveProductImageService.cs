using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.commands.RemoveProductImage
{
    public interface IRemoveProductImageService
    {
        ResultDto Execute(long ImageId);
    }

    public class RemoveProductImageService : IRemoveProductImageService
    {
        private readonly IDataBaseContext _context;

        public RemoveProductImageService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long ImageId)
        {
            var image = _context.ProductImages.Find(ImageId);
            if (image == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "تصویر یافت نشد"
                };
            }

            image.IsRemoved = true;
            image.RemoveTime = DateTime.Now;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "تصویر با موفقیت حذف شد"
            };
        }
    }
}
