using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetOrdersByStudentId(int studentId,string search);
        Task<IEnumerable<OrderDetailModel>> GetDetailsByOrderId(int orderId);
        Task<OrderModel> GetOrderByStudentId(int studentId);
        Task<OrderModel> GetOrderByOrderId(int orderId);
        void AddOrder(OrderModel order);
        Task<OrderDetailModel> GetDetailByIds(int orderId,int testId);
        void AddDetail(OrderDetailModel detail);
        Task<bool> CheckByTest(int studentId,int testId);
        Task<IEnumerable<OrderDetailModel>> GetAllDetailsByStudentId(int studentId);
        Task<double> GetOrderPriceByStudentId(int studentId);
        Task<OrderDetailModel> GetDetailById(int detailId);
        void RemoveDetail(OrderDetailModel detail);
        Task<int> DetailsCountByOrderId(int orderId);
        void UpdateOrder(OrderModel order);
        void RemoveOrder(OrderModel order);
        void Save();
    }
}