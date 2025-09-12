using Store.Common.Dto;
using System.Collections.Generic;

namespace Store.Application.Services.HomePages.Queries.GetImagesAdmin
{
    public interface IGetImagesAdminService
    {
        ResultDto<List<ImageAdminDto>> Execute();
    }
}
