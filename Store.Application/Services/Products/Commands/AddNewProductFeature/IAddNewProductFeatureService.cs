using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.AddNewProductFeature
{
    public interface IAddNewProductFeatureService
    {
        ResultDto Execute(RequestAddNewProductFeatureDto request);
    }

    public class AddNewProductFeatureService : IAddNewProductFeatureService
    {
        private readonly IDataBaseContext _context;

        public AddNewProductFeatureService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewProductFeatureDto request)
        {
            try
            {
                // ✅ بررسی خالی نبودن عنوان و مقدار
                if (string.IsNullOrWhiteSpace(request.DisplayName))
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عنوان ویژگی نمی‌تواند خالی باشد"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Value))
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "مقدار ویژگی نمی‌تواند خالی باشد"
                    };
                }

                var product = _context.Products.Find(request.ProductId);
                if (product == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "محصول یافت نشد"
                    };
                }

                var feature = new ProductFeatures
                {
                    Product = product,
                    ProductId = request.ProductId,
                    DisplayName = request.DisplayName.Trim(),
                    Value = request.Value.Trim(),
                };

                _context.ProductFeatures.Add(feature);
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "ویژگی جدید با موفقیت به محصول اضافه شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد هنگام افزودن ویژگی"
                };
            }
        }
    }

    public class RequestAddNewProductFeatureDto
    {
        public long ProductId { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
