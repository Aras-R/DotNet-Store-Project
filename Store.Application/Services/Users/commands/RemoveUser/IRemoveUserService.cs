using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.commands.RemoveUser
{
    public interface IRemoveUserService
    {
        ResultDto Execute(long UserId);
    }
    
}
