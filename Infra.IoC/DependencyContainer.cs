using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Domin.Interfaces;
using Application.Services;
using Application.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace IOC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
             //Data Layer
            service.AddScoped<IGradeRepository, GradeRepository>();
            service.AddScoped<ILessonRepository, LessonRepository>();
            service.AddScoped<ITeacherRepository, TeacherRepository>();
            service.AddScoped<IStudentRepository,StudentRepository>();
            service.AddScoped<ITestRepository,TestRepository>();
            service.AddScoped<IQuestionRepository, QuestionRepository>();
            service.AddScoped<IAnswerRepository, AnswerRepository>();
            service.AddScoped<IWorkBookRepository, WorkBookRepository>();
            service.AddScoped<ILevelPercentRepository, LevelPercentRepository>();
            service.AddScoped<IDashboardRepository, DashboardRepository>();
            service.AddScoped<IAdminRepository, AdminRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IMerchantRepository, MerchantRepository>();
            //Application Layer 
            service.AddScoped<IGradeService,GradeService>();
            service.AddScoped<ILessonService, LessonService>();
            service.AddScoped<ITeacherService, TeacherService>();
            service.AddScoped<IStudentService, StudentService>();
            service.AddScoped<ITestService, TestService>();
            service.AddScoped<IQuestionService, QuestionService>();
            service.AddScoped<IMasterDashboardService,MasterDashboardService>();
            service.AddScoped<IAnswerService, AnswerService>();
            service.AddScoped<IWorkBookService, WorkBookService>();
            service.AddScoped<ILevelPercentService, LevelPercentService>();
            service.AddScoped<IDashboardService, DashboardService>();
            service.AddScoped<IAdminService, AdminService>();
            service.AddSingleton(typeof(IConverter),
                new SynchronizedConverter(new PdfTools()));
            service.AddScoped<IReportService, ReportService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IMerchantService, MerchantService>();
        }
    }
}