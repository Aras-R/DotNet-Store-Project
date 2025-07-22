using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Users.commands.RegisterUser;
using Store.Application.Services.Users.commands.RemoveUser;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Queries.GetUsers;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {

        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRemoveUserService _removeUserService;
        public UsersController(
              IGetUsersService getUsersService
            , IGetRolesService getRolesService
            , IRegisterUserService registerUserService
            , IRemoveUserService removeUserService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _removeUserService = removeUserService;
        }

        public IActionResult Index(string serchkey, int page = 1)
        {
            return View(_getUsersService.Execute(new RequestGetUserDto
            {
                Page = page,
                SearchKey = serchkey,
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
        {
            var result = _registerUserService.Execute(new RequestRegisterUserDto
            {
                Email = Email,
                FullName = FullName,
                roles = new List<RolesInRegisterUserDto>()
                   {
                        new RolesInRegisterUserDto
                        {
                             Id= RoleId
                        }
                   },
                Password = Password,
                RePasword = RePassword,
            });
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_removeUserService.Execute(UserId));
        }

    }

}
