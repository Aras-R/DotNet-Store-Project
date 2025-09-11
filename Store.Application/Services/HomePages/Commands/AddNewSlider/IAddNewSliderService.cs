using Store.Application.Interfaces.contexts;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePages.Commands.AddNewSlider
{
    public interface IAddNewSliderService
    {
        ResultDto Execute(IFormFile file, string Link);
    }

    public class AddNewSliderService : IAddNewSliderService
    {

        private readonly IHostingEnvironment _environment;
        private readonly IDataBaseContext _context;

        public AddNewSliderService(IHostingEnvironment environment, IDataBaseContext context)
        {
            _environment = environment;
            _context = context;
        }
        public ResultDto Execute(IFormFile file, string Link)
        {
            var resultUpload = UploadFile(file);


            Slider slider = new Slider()
            {
                link = Link,
                Src = resultUpload.FileNameAddress,
            };
            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true
            };

    }
    private UploadDto UploadFile(IFormFile file)
    {
        if (file != null)
        {
            string folder = $@"images\HomePages\Slider\";
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }


            if (file == null || file.Length == 0)
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
        return null;
    }
}



}
