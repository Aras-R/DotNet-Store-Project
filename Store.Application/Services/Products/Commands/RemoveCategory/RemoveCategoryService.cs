using Store.Application.Interfaces.contexts;
using Store.Application.Services.Products.commands.RemoveCategory;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.commands.RemoveCategory
{ 
    public class RemoveCategoryService : IRemoveCategoryService
    {
        private readonly IDataBaseContext _context;

        public RemoveCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long CategoryId)
        {
            var category = _context.Categories.Find(CategoryId);
            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "دسته‌بندی یافت نشد"
                };
            }

            category.IsRemoved = true;
            category.RemoveTime = DateTime.Now;

       
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "دسته‌بندی با موفقیت حذف شد"
            };
        }
    }
}


