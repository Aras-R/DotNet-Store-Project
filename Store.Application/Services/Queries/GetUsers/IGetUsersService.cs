using Bugeto_Store.Application.Services.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ReslutGetUserDto Execute(RequestGetUserDto request);
    }
}
