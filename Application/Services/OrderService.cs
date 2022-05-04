using Application.Interfaces;
using Application.Others;
using Application.ViewModels.OrderViewModel;
using Domin.Interfaces;
using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITestRepository _testRepository;
        public OrderService(IOrderRepository orderRepository,ITestRepository testRepository)
        {
            _orderRepository = orderRepository;
            _testRepository = testRepository;
        }
        public async Task<OrderViewModel> GetOrderByStudentId(int studentId)
        {
            var order = await _orderRepository.GetOrderByStudentId(studentId);
            OrderViewModel model = new OrderViewModel();
            if (order!= null)
            {
                model.IsFinally = order.IsFinally;
                model.OrderId = order.OrderId;
                model.OrderCode = order.OrderCode;
                model.StudentId = order.StudentId;
                model.CreateDate = order.CreateDate;
                return model;
            }
            model = null;
            return model;
        }
        public async Task<OrderViewModel> GetOrderByOrderId(int orderId)
        {
            var order = await _orderRepository.GetOrderByOrderId(orderId);
            OrderViewModel model = new OrderViewModel();
            if (order != null)
            {
                model.IsFinally = order.IsFinally;
                model.OrderId = order.OrderId;
                model.OrderCode = order.OrderCode;
                model.StudentId = order.StudentId;
                model.CreateDate = order.CreateDate;
                return model;
            }
            model = null;
            return model;
        }
        public void AddOrderByStudentId(int studentId)
        {
            OrderModel model = new OrderModel();
            model.CreateDate = DateTime.Parse(DateTime.Now.ToShamsi());
            model.StudentId = studentId;
            model.OrderCode = RandomNumber.Random().ToString();
            _orderRepository.AddOrder(model);
        }
        public async Task<OrderDetailViewModel> GetDetailByIds(int orderId, int testId)
        {
            var detail = await _orderRepository.GetDetailByIds(orderId,testId);
            var test = await _testRepository.GetTestById(testId);
            OrderDetailViewModel model = new OrderDetailViewModel();
            if (detail != null)
            {
                model.OrderId = orderId;
                model.DetailId = detail.DetailId;
                model.Price = test.TestPrice;
                model.TestId = testId;
                return model;
            }
            model = null;
            return model;
        }
        public void AddDetail(int orderId, int testId)
        {
            var test = _testRepository.GetTestById(testId).Result;
            OrderDetailModel detail = new OrderDetailModel();
            detail.TestId = testId;
            detail.OrderId = orderId;
            detail.Price = test.TestPrice;
            _orderRepository.AddDetail(detail);
        }
        public Tuple<List<ShowBasketViewModel>, double,string> ShowBasket(int studentId)
        {
            var order = _orderRepository.GetOrderByStudentId(studentId).Result;
            var details = _orderRepository.GetAllDetailsByStudentId(studentId).Result;
            double orderPrice = _orderRepository.GetOrderPriceByStudentId(studentId).Result;
            List<ShowBasketViewModel> models = new List<ShowBasketViewModel>();
            foreach (var detail in details)
            {
                models.Add(new ShowBasketViewModel() 
                {
                    TestTitle = detail.TestModel.TestTitle,
                    TestCode = detail.TestModel.TestCode,
                    StartTest = detail.TestModel.StartTest.ToString("HH:mm"),
                    EndTest = detail.TestModel.EndTest.ToString("HH:mm"),
                    TestDayTime = detail.TestModel.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = detail.TestId,
                    OrderId = detail.OrderId,
                    TestPrice = detail.Price.ToString("#,0"),
                    DetailId = detail.DetailId
                });
            }
            string orderCode = (order == null) ? (orderCode = "") : (orderCode = order.OrderCode);
            return Tuple.Create(models,orderPrice, orderCode);
        }
        public void RemoveDetailById(int detailId)
        {
            var detail = _orderRepository.GetDetailById(detailId).Result;
            _orderRepository.RemoveDetail(detail);
        }
        public async Task<int> DetailsCountByOrderId(int orderId)
        {
            return await _orderRepository.DetailsCountByOrderId(orderId);
        }
        public void UpdateOrder(OrderViewModel order)
        {
            OrderModel model = new OrderModel();
            model.CreateDate = order.CreateDate;
            model.IsFinally = true;
            model.OrderCode = order.OrderCode;
            model.OrderId = order.OrderId;
            model.StudentId = order.StudentId;
            _orderRepository.UpdateOrder(model);
        }
        public void RemoveOrderByOrderId(int orderId)
        {
            var order = _orderRepository.GetOrderByOrderId(orderId).Result;
            _orderRepository.RemoveOrder(order);
        }
    }
}