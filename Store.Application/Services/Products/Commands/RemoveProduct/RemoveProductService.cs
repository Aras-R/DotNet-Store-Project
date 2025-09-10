using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.commands.RemoveProduct
{
    public class RemoveProductService : IRemoveProductService
    {
        private readonly IDataBaseContext _context;

        public RemoveProductService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto Execute(long ProductId)
        {
            var product = _context.Products.Find(ProductId);
            if (product == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد"
                };
            }

            product.IsRemoved = true;
            product.RemoveTime = DateTime.Now;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "محصول با موفقیت حذف شد"
            };
        }
    }
}


