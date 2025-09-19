using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.commands.EditProductFeature
{
    public interface IEditProductFeatureService
    {
        ResultDto Execute(RequestEditProductFeatureDto request);
    }

    public class EditProductFeatureService : IEditProductFeatureService
    {
        private readonly IDataBaseContext _context;

        public EditProductFeatureService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditProductFeatureDto request)
        {
            var feature = _context.ProductFeatures.Find(request.FeatureId);
            if (feature == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "ویژگی یافت نشد"
                };
            }

            if (string.IsNullOrWhiteSpace(request.DisplayName))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نام ویژگی نمی‌تواند خالی باشد"
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

            // ویرایش
            feature.DisplayName = request.DisplayName;
            feature.Value = request.Value;
            feature.UpdateTime = DateTime.Now;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویژگی محصول با موفقیت ویرایش شد"
            };
        }
    }

    public class RequestEditProductFeatureDto
    {
        public long FeatureId { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
