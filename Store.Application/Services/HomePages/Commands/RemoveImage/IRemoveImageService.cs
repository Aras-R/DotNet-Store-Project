using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePages.commands.RemoveImage
{
    public interface IRemoveImageService
    {
        ResultDto Execute(long ImageId);
    }
    
}
