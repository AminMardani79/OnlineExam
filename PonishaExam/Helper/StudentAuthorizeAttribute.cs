using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PonishaExam.Helper
{
    public class StudentAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("دسترسی به این بخش از سایت امکان پذیر نیست");
            if (context.HttpContext.User.GetRoleId() != "3")
                throw new UnauthorizedAccessException("دسترسی به پنل دانش آموزان برای شما موجود نیست");
        }
    }
}
