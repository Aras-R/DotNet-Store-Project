using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePages.Queries.GetSliders
{
    public class GetSlidersService : IGetSlidersService
    {
        private readonly IDataBaseContext _context;

        public GetSlidersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<SliderDto>> Execute()
        {
            var sliders = _context.Sliders
                .OrderByDescending(p => p.Id)
                .Select(p => new SliderDto
                {
                    Id = p.Id,
                    Src = p.Src,
                    Link = p.link, 
                    InsertTime = p.InsertTime
                }).ToList();

            return new ResultDto<List<SliderDto>>
            {
                Data = sliders,
                IsSuccess = true
            };
        }
    }
}
