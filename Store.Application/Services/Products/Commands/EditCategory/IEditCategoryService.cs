using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.commands.EditCategory
{
    public interface IEditCategoryService
    {
        ResultDto Execute(RequestEditCategoryDto request);
    }

    public class EditCategoryService : IEditCategoryService
    {
        private readonly IDataBaseContext _context;

        public EditCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditCategoryDto request)
        {
            var category = _context.Categories.Find(request.CategoryId);
            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "دسته‌بندی یافت نشد"
                };
            }

            // چک خالی بودن نام
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نام دسته‌بندی نمی‌تواند خالی باشد"
                };
            }

            category.Name = request.Name;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویرایش دسته‌بندی با موفقیت انجام شد"
            };
        }
    }

    public class RequestEditCategoryDto
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
    }
}
