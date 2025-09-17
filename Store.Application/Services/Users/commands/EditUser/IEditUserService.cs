using Store.Application.Interfaces.contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.commands.EditUser
{
    public interface IEditUserService
    {
        ResultDto Execute(RequestEditUserDto request);
    }

    public class EditUserService : IEditUserService
    {
        private readonly IDataBaseContext _context;

        public EditUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditUserDto request)
        {
            var user = _context.Users.Find(request.UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد"
                };
            }

            // چک خالی بودن نام
            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نام و نام خانوادگی نمی‌تواند خالی باشد"
                };
            }

            // چک خالی بودن و فرمت ایمیل
            if (string.IsNullOrWhiteSpace(request.Email) ||
                !Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "ایمیل نامعتبر است"
                };
            }
            user.FullName = request.FullName;
            user.Email = request.Email;

            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ویرایش کاربر با موفقیت انجام شد"
            };
        }
    }

    public class RequestEditUserDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
