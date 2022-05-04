using Application.ViewModels.OrderViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderViewModel> GetOrderByStudentId(int studentId);
        Task<OrderViewModel> GetOrderByOrderId(int orderId);
        void AddOrderByStudentId(int studentId);
        Task<OrderDetailViewModel> GetDetailByIds(int orderId, int testId);
        void AddDetail(int orderId,int testId);
        Tuple<List<ShowBasketViewModel>, double, string> ShowBasket(int studentId);
        void RemoveDetailById(int detailId);
        Task<int> DetailsCountByOrderId(int orderId);
        void UpdateOrder(OrderViewModel order);
        void RemoveOrderByOrderId(int orderId);
    }
}