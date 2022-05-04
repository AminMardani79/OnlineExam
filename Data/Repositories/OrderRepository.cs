using Domin.Interfaces;
using Domin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ExamContext _context;

        public OrderRepository(ExamContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderModel>> GetOrdersByStudentId(int studentId,string search)
        {
            return await _context.OrderModel.Include(n=> n.StudentModel).Where(n => n.StudentId == studentId && EF.Functions.Like(n.StudentModel.StudentName,$"%{search}%")).ToListAsync();
        }
        public async Task<IEnumerable<OrderDetailModel>> GetDetailsByOrderId(int orderId)
        {
            return await _context.OrderDetailModels.Include(n=> n.TestModel).Where(n=> n.OrderId == orderId).ToListAsync();
        }
        public async Task<OrderModel> GetOrderByStudentId(int studentId)
        {
            return await _context.OrderModel.SingleOrDefaultAsync(n=> n.StudentId == studentId && !n.IsFinally);
        }
        public async Task<OrderModel> GetOrderByOrderId(int orderId)
        {
            return await _context.OrderModel.SingleOrDefaultAsync(n=> n.OrderId == orderId);
        }
        public void AddOrder(OrderModel order)
        {
            _context.Add(order);Save();
        }
        public async Task<OrderDetailModel> GetDetailByIds(int orderId, int testId)
        {
            return await _context.OrderDetailModels.SingleOrDefaultAsync(n=> n.OrderId == orderId && n.TestId == testId);
        }
        public void AddDetail(OrderDetailModel detail)
        {
            _context.Add(detail);
            Save();
        }
        public async Task<bool> CheckByTest(int studentId, int testId)
        {
            return await _context.OrderDetailModels.Include(n=> n.Order).AnyAsync(n=> n.TestId == testId && n.Order.StudentId == studentId && n.Order.IsFinally);
        }
        public async Task<IEnumerable<OrderDetailModel>> GetAllDetailsByStudentId(int studentId)
        {
            return await _context.OrderDetailModels.Include(n=> n.Order).Include(n=> n.TestModel).Where(n => n.Order.StudentId == studentId && !n.Order.IsFinally).ToListAsync();
        }
        public async Task<double> GetOrderPriceByStudentId(int studentId)
        {
            return await _context.OrderDetailModels.Include(n => n.Order).Where(n => n.Order.StudentId == studentId && !n.Order.IsFinally).SumAsync(n=> n.Price);
        }
        public async Task<OrderDetailModel> GetDetailById(int detailId)
        {
            return await _context.OrderDetailModels.SingleOrDefaultAsync(n=> n.DetailId == detailId);
        }
        public void RemoveDetail(OrderDetailModel detail)
        {
            _context.Remove(detail);Save();
        }
        public async Task<int> DetailsCountByOrderId(int orderId)
        {
            return await _context.OrderDetailModels.CountAsync(n=> n.OrderId == orderId);
        }
        public void UpdateOrder(OrderModel order)
        {
            _context.Update(order);Save();
        }
        public void RemoveOrder(OrderModel order)
        {
            _context.Remove(order);Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}