using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ExamContext : DbContext
    {
        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {

        }
        public DbSet<GradeModel> GradeModels { get; set; }
        public DbSet<LessonModel> LessonModels { get; set; }
        public DbSet<TeacherModel> TeacherModels { get; set; }
        public DbSet<TeacherLessonModel> TeacherLessonModels { get; set; }
        public DbSet<StudentModel> StudentModels { set; get; }
        public DbSet<TestModel> TestModels { set; get; }
        public DbSet<QuestionModel> QuestionModels { set; get; }
        public DbSet<MasterWithStudentModel> MasterWithStudent { set; get; }
        public DbSet<TestStudentsModel> TestStudentsModels { get; set; }
        public DbSet<AnswerModel> AnswerModels { get; set; }
        public DbSet<WorkBookModel> WorkBookModels { get; set; }
        public DbSet<LevelPercentModel> LevelPercentModels { get; set; }
        public DbSet<AdminModel> AdminModels { get; set; }
        public DbSet<OrderModel> OrderModel { get; set; }
        public DbSet<OrderDetailModel> OrderDetailModels { get; set; }
        public DbSet<MerchantModel> MerchantModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var mutableForeignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                mutableForeignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<MasterWithStudentModel>().HasKey(k => new { k.StudentId, k.TeacherId });
            modelBuilder.Entity<GradeModel>().HasQueryFilter(p => p.IsGradeDelete == false);
            modelBuilder.Entity<LessonModel>().HasQueryFilter(p => p.IsLessonDelete == false);
            modelBuilder.Entity<TeacherModel>().HasQueryFilter(p => p.IsTeacherDeleted == false);
            modelBuilder.Entity<StudentModel>().HasQueryFilter(p => p.IsStudentDelete == false);
            modelBuilder.Entity<TestModel>().HasQueryFilter(q => q.IsDelete == false);
            modelBuilder.Entity<AdminModel>().HasQueryFilter(q => q.IsAdminDeleted == false);
            modelBuilder.Entity<TeacherLessonModel>().HasKey(tl => new { tl.LessonId, tl.TeacherId });

            #region DeleteBehavior
            // Remove TestModel
            modelBuilder.Entity<QuestionModel>()
        .HasOne(p => p.Test)
        .WithMany(b => b.Question)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestStudentsModel>()
        .HasOne(p => p.TestModel)
        .WithMany(b => b.TestStudentsModels)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnswerModel>()
        .HasOne(p => p.TestModel)
        .WithMany(b => b.AnswerModels)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkBookModel>()
        .HasOne(p => p.TestModel)
        .WithMany(b => b.WorkBookModels)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LevelPercentModel>()
        .HasOne(p => p.TestModel)
        .WithMany(b => b.LevelPercentModels)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetailModel>()
       .HasOne(p => p.TestModel)
       .WithMany(b => b.OrderDetails)
       .OnDelete(DeleteBehavior.Cascade);

            // Remove StudentModel
            modelBuilder.Entity<LevelPercentModel>()
        .HasOne(p => p.StudentModel)
        .WithMany(b => b.LevelPercentModels)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkBookModel>()
        .HasOne(p => p.StudentModel)
        .WithMany(b => b.WorkBookModels)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnswerModel>()
        .HasOne(p => p.StudentModel)
        .WithMany(b => b.AnswerModels)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MasterWithStudentModel>()
        .HasOne(p => p.Student)
        .WithMany(b => b.MasterWithStudent)
        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderModel>()
       .HasOne(p => p.StudentModel)
       .WithMany(b => b.OrderModels)
       .OnDelete(DeleteBehavior.Cascade);

            // Remove Grade
            modelBuilder.Entity<LessonModel>()
        .HasOne(p => p.Grade)
        .WithMany(b => b.Lessons)
        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StudentModel>()
        .HasOne(p => p.Grade)
        .WithMany(b => b.Students)
        .OnDelete(DeleteBehavior.Cascade);
            // Remove Teacher
            modelBuilder.Entity<MasterWithStudentModel>()
        .HasOne(p => p.Teacher)
        .WithMany(b => b.MasterWithStudent)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeacherLessonModel>()
        .HasOne(p => p.TeacherModel)
        .WithMany(b => b.TeacherLessonModels)
        .OnDelete(DeleteBehavior.Cascade);

            // Remove LessonModel
            modelBuilder.Entity<TeacherLessonModel>()
        .HasOne(p => p.LessonModel)
        .WithMany(b => b.TeacherLessonModels)
        .OnDelete(DeleteBehavior.Cascade);

            // Remove Order
            modelBuilder.Entity<OrderDetailModel>()
        .HasOne(p => p.Order)
        .WithMany(b => b.OrderDetailModel)
        .OnDelete(DeleteBehavior.Cascade);

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}