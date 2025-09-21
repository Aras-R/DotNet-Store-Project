using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.commands.RemoveProductFeature
{
    public interface IRemoveProductFeatureService
    {
        ResultDto Execute(long FeatureId);
    }

    public class RemoveProductFeatureService : IRemoveProductFeatureService
    {
        private readonly IDataBaseContext _context;

        public RemoveProductFeatureService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long FeatureId)
        {
            var feature = _context.ProductFeatures.Find(FeatureId);
            if (feature == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "ویژگی محصول یافت نشد"
                };
            }

            feature.IsRemoved = true;
            feature.RemoveTime = DateTime.Now;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویژگی محصول با موفقیت حذف شد"
            };
        }
    }
}
