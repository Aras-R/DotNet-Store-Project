using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto Execute(RequestEditProductDto request);
    }

    public class EditProductService : IEditProductService
    {
        private readonly IDataBaseContext _context;

        public EditProductService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditProductDto request)
        {
            var product = _context.Products.Find(request.ProductId);
            if (product == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد"
                };
            }

            // چک نام
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نام محصول نمی‌تواند خالی باشد"
                };
            }

            // چک برند
            if (string.IsNullOrWhiteSpace(request.Brand))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "برند محصول نمی‌تواند خالی باشد"
                };
            }

            // چک قیمت
            if (request.Price <= 0)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "قیمت محصول باید بزرگ‌تر مساوی صفر باشد"
                };
            }

            // چک موجودی
            if (request.Inventory <= 0)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "موجودی محصول نمی‌تواند منفی باشد"
                };
            }

            // چک دسته بندی
            var category = _context.Categories.Find(request.CategoryId);
            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "دسته‌بندی معتبر یافت نشد"
                };
            }

            // اعمال تغییرات
            product.Name = request.Name;
            product.Brand = request.Brand;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Inventory = request.Inventory;
            product.Displayed = request.Displayed;
            product.CategoryId = request.CategoryId;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویرایش محصول با موفقیت انجام شد"
            };
        }
    }

    public class RequestEditProductDto
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public long CategoryId { get; set; }
    }
}
