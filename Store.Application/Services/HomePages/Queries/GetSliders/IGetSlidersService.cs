using Store.Common.Dto;
using System.Collections.Generic;

namespace Store.Application.Services.HomePages.Queries.GetSliders
{
    public interface IGetSlidersService
    {
        ResultDto<List<SliderDto>> Execute();
    }
}
