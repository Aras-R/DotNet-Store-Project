using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Store.Application.Services.HomePages.Queries.GetImagesAdmin
{
    public class GetImagesAdminService : IGetImagesAdminService
    {
        private readonly IDataBaseContext _context;

        public GetImagesAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ImageAdminDto>> Execute()
        {
            var images = _context.HomePageImages
                .OrderByDescending(p => p.Id)
                .Select(p => new ImageAdminDto
                {
                    Id = p.Id,
                    Src = p.Src,
                    Link = p.link,
                    ImageLocation = p.ImageLocation.ToString(),
                    InsertTime = p.InsertTime
                }).ToList();

            return new ResultDto<List<ImageAdminDto>>
            {
                Data = images,
                IsSuccess = true
            };
        }
    }
}
