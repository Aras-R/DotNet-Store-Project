using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Store.Application.Services.Products.Commands.AddNewProductImage
{
    public interface IAddNewProductImageService
    {
        ResultDto Execute(RequestAddNewProductImageDto request);
    }

    public class AddNewProductImageService : IAddNewProductImageService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;

        public AddNewProductImageService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(RequestAddNewProductImageDto request)
        {
            if (request == null || request.ProductId <= 0 || request.Image == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات تصویر معتبر نیست"
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

            // ذخیره تصویر
            var uploadedResult = UploadFile(request.Image);

            if (!uploadedResult.Status)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطا در بارگذاری تصویر"
                };
            }

            var newImage = new ProductImages
            {
                ProductId = request.ProductId,
                Src = uploadedResult.FileNameAddress
            };

            _context.ProductImages.Add(newImage);
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "تصویر با موفقیت اضافه شد"
            };
        }

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                if (file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return new UploadDto { Status = false };
        }
    }

    public class RequestAddNewProductImageDto
    {
        public long ProductId { get; set; }
        public IFormFile Image { get; set; }
    }

    public class UploadDto
    {
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}
