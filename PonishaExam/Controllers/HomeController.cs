using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PonishaExam.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.AccountViewModel;
using Application.ViewModels.TeacherViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using PonishaExam.Helper;
using Application.ViewModels.AdminViewModel;
using RestSharp;

namespace PonishaExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IAdminService _adminService;
        private readonly IOrderService _orderService;
        private string authority;

        public HomeController(IStudentService studentService, ITeacherService teacherService,IAdminService adminService,IOrderService orderService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _adminService = adminService;
            _orderService = orderService;
        }

        #region LoginStudent

        [HttpGet]
        [Route("/")]
        public IActionResult LoginStudent(string errorAlert)
        {
            if (User.Identity.IsAuthenticated)
            {
                int roleId = int.Parse(User.GetRoleId());
                if (roleId == 3)
                {
                    return Redirect("/StudentPanel");
                }
                else if (roleId == 2)
                {
                    return Redirect("/Master");
                }
                else if (roleId == 1)
                {
                    return Redirect("/AdminDashboard");
                }
            }
            ViewBag.ErrorAlert = errorAlert;
            return View();
        }

        [HttpPost]
        [Route("/")]
        public IActionResult LoginStudent(LoginStudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            var user = _studentService.LoginStudent(student).Result;
            if (user == null)
            {
                string errorAlert = "کاربری با اطلاعات وارد شده یافت نشد";
                return RedirectToAction("LoginStudent", "Home", new { errorAlert = errorAlert });
            }
            if (user.Active == true)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.StudentName),
                    new Claim(ClaimTypes.NameIdentifier,user.StudentId.ToString()),
                    new Claim("PhoneNumber",user.StudentPhoneNumber),
                    new Claim("Email",user.StudentMail),
                    new Claim("StudentImage",user.StudentImage),
                    new Claim("NationalCode",user.StudentNationalCode),
                    new Claim("StudentPassword",user.StudentPassword),
                    new Claim("GradeName",user.GradeName),
                    new Claim("GradeId",user.GradeId.ToString()),
                    new Claim("RoleId",user.RoleId.ToString()),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var property = new AuthenticationProperties()
                {
                    IsPersistent = student.RememberMe
                };
                HttpContext.SignInAsync(principal, property);
                return Redirect("/StudentPanel");
            }
            else
            {
                ModelState.AddModelError("StudentPassword", "کاربری با کد ملی وارد شده یافت نشد");
                return View(student);
            }
        }
        #endregion

        #region Logout Student
        [Route("/Logout")]
        public IActionResult LogoutStudent()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

        #region LoginMaster

        [Route("/MasterLogin")]
        [HttpGet]
        public IActionResult MasterLogin()
        {
            return View();
        }
        [HttpPost]
        [Route("/MasterLogin")]
        public IActionResult MasterLogin(RequestLoginViewModel model,string master)
        {
            ViewBag.Master = master;
            if (ModelState.IsValid)
            {
                var check = _teacherService.ExistTeacher(model.LoginMaster).Result;
                if (check)
                {
                    var teacher = _teacherService.GetTeacherLoginInfo(model.LoginMaster).Result;
                    if (teacher != null)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,teacher.TeacherName),
                            new Claim(ClaimTypes.NameIdentifier,teacher.TeacherId.ToString()),
                            new Claim("TeacherImage",teacher.TeacherImage),
                            new Claim("TeacherEmail",teacher.TeacherEmail),
                            new Claim("RoleId",teacher.RoleId.ToString()),
                            new Claim("NationalCode",teacher.NationalCode)

                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var property = new AuthenticationProperties()
                        {
                            IsPersistent = model.LoginMaster.SaveMe
                        };
                        HttpContext.SignInAsync(principal, property);
                        return Redirect("/Master");
                    }
                    else
                    {
                        ViewBag.error = "حساب شما فعال نیست  به مدیر سامانه پیام ارسال کنید";

                        return View(model);
                    }
                }
                else
                {
                    ViewBag.error = "حساب کاربری یافت نشد";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        #endregion

        #region LogoutMaster

        [Route("/LogoutMaster")]
        public IActionResult LogoutMaster()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/MasterLogin");
        }

        #endregion

        #region LoginAdmin
        [Route("/AdminLogin")]
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [Route("/AdminLogin")]
        public IActionResult AdminLogin(RequestLoginViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var check = _adminService.IsExistAdmin(model.LoginAdmin).Result;
                if (check)
                {
                    var teacher = _adminService.GetAdminLoginInfo(model.LoginAdmin).Result;
                    if (teacher != null)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,teacher.AdminName),
                            new Claim(ClaimTypes.NameIdentifier,teacher.AdminId.ToString()),
                            new Claim("AdminImage",teacher.AdminImage),
                            new Claim("AdminEmail",teacher.AdminEmail),
                            new Claim("RoleId",teacher.RoleId.ToString()),
                            new Claim("NationalCode",teacher.AdminNationalCode)
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var property = new AuthenticationProperties()
                        {
                            IsPersistent = model.LoginAdmin.SaveMe
                        };
                        HttpContext.SignInAsync(principal, property);
                        return Redirect("/AdminDashboard");
                    }
                    else
                    {
                        ViewBag.error = "حساب شما فعال نیست  به مدیر سامانه پیام ارسال کنید";
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.error = "حساب کاربری یافت نشد";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region LogoutAdmin

        [Route("/LogoutAdmin")]
        public IActionResult LogoutAdmin()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/AdminLogin");
        }

        #endregion

        #region Error404

        [Route("/404")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        [HttpGet]
        [Route("/Home/Verify/{orderId}/{orderPrice}")]
        public IActionResult Verify(int orderId,double orderPrice)
        {
            var order = _orderService.GetOrderByOrderId(orderId).Result;
            var amount = orderPrice * 10;
            string merchant = "27e232d6-b9e3-11e9-96ac-000c295eb8fc";
            try
            {
                if (HttpContext.Request.Query["Authority"] != "")
                {
                    authority = HttpContext.Request.Query["Authority"];
                }
                string url = "https://api.zarinpal.com/pg/v4/payment/verify.json?merchant_id=" +
                             merchant + "&amount="
                             + amount + "&authority="
                             + authority;
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);
                Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                string data = jodata["data"].ToString();
                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                string errors = jo["errors"].ToString();
                if (data != "[]")
                {
                    string refid = jodata["data"]["ref_id"].ToString();
                    ViewBag.code = refid;
                    ViewBag.Price = orderPrice;
                    ViewBag.OrderCode = order.OrderCode;
                    _orderService.UpdateOrder(order);
                    return View();
                }
                else if (errors != "[]")
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return NotFound();
        }
    }
}