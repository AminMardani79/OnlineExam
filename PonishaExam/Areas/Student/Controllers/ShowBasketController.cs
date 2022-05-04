using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PonishaExam.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PonishaExam.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    public class ShowBasketController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMerchantService _merchantService;
        private string authority;
        public ShowBasketController(IOrderService orderService,IMerchantService merchantService)
        {
            _orderService = orderService;
            _merchantService = merchantService;
        }
        [HttpGet]
        [Route("/StudentPanel/Basket")]
        public IActionResult Index()
        {
            int studentId = int.Parse(User.GetStudentId());
            var details = _orderService.ShowBasket(studentId);
            return View(details);
        }
        [HttpGet]
        [Route("/AddToBasket")]
        public JsonResult AddToBasket(int id)
        {
            int studentId = int.Parse(User.GetStudentId());
            var order = _orderService.GetOrderByStudentId(studentId).Result;
            if (order == null)
            {
                _orderService.AddOrderByStudentId(studentId);
            }
            int orderId = _orderService.GetOrderByStudentId(studentId).Result.OrderId;
            var detail = _orderService.GetDetailByIds(orderId, id).Result;
            if (detail != null)
            {
                return Json("1");
            }
            else
            {
                _orderService.AddDetail(orderId, id);
                return Json("2");
            }
        }
        [HttpGet]
        [Route("/ShowBasket/Remove/{id}")]
        public void RemoveFromBasket(int id)
        {
            _orderService.RemoveDetailById(id);
        }
        [Route("/Payment/{orderPrice}")]
        public IActionResult Payment(double orderPrice)
        {
            int studentId = int.Parse(User.GetStudentId());
            var order = _orderService.GetOrderByStudentId(studentId).Result;
            var firstMerchant = _merchantService.GetFirshMerchant().Result;
            string email = User.GetEmail();
            string mobile = User.GetPhoneNumber();
            var amount = orderPrice * 10;
            string merchant = firstMerchant.MerchantKey;
            string callbackurl = $"https://psdstudio.ir/Home/Verify/{order.OrderId}/{orderPrice}";
            string description = "پرداخت نهایی آزمون";
            try
            {
                string[] metadata = new string[2];
                metadata[0] = $"[mobile: {mobile}]";
                metadata[1] = $"[email: {email}]";

                //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
                //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

                string requesturl;
                requesturl = "https://api.zarinpal.com/pg/v4/payment/request.json?merchant_id=" +
                    merchant + "&amount=" + amount +
                    "&callback_url=" + callbackurl +
                    "&description=" + description +
                    "&metadata[0]=" + metadata[0] + "& metadata[1]=" + metadata[1];
                ;
                var client = new RestClient(requesturl);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                IRestResponse requestresponse = client.Execute(request);
                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content);
                string errorscode = jo["errors"].ToString();
                Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content);
                string dataauth = jodata["data"].ToString();
                if (_orderService.DetailsCountByOrderId(order.OrderId).Result == 0)
                {
                    return Redirect("/StudentPanel/Basket");
                }
                if (authority != "[]")
                {
                    authority = jodata["data"]["authority"].ToString();
                    string gatewayUrl = "https://www.zarinpal.com/pg/StartPay/" + authority;
                    return Redirect(gatewayUrl);
                }
                else
                {
                    //return BadRequest();
                    return BadRequest("error ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
    }
    
}