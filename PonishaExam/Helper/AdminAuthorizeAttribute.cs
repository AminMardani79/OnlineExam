using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PonishaExam.Helper
{
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("دسترسی به این بخش از سایت امکان پذیر نیست");
            if (context.HttpContext.User.GetRoleId() != "1")
                throw new UnauthorizedAccessException("دسترسی به پنل ادمین برای شما موجود نیست");
        }
    }
}