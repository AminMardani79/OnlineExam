using Application.ViewModels.WorkBookViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Others
{
    public static class CreateTable
    {
        public static StringBuilder HtmlTable(List<WorkBookViewModel> model)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[10]
            {
                new DataColumn("بالاترین تراز", typeof(double)),
                new DataColumn("تراز", typeof(double)),
                new DataColumn("رتبه", typeof(int)),
                new DataColumn("بالاترین درصد", typeof(double)),
                new DataColumn("درصد", typeof(double)),
                new DataColumn("سفید", typeof(int)),
                new DataColumn("غلط", typeof(int)),
                new DataColumn("صحیح", typeof(int)),
                new DataColumn("تعداد سوال", typeof(int)),
                new DataColumn("نام درس", typeof(string)),
            });
            foreach (var item in model)
            {
                dt.Rows.Add(item.HighestLevel,item.Level,item.Rank,item.HighestPercent,item.Percent,item.NoCheckedAnswers
                    ,item.WrongAnswers,item.CorrectAnswers,item.TestCounts,item.LessonName);
            }

            StringBuilder sb = new StringBuilder();
            //Table start.
            sb.Append(
                "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%'>");

            //Adding HeaderRow.
            sb.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<th style='background-color: #cee2ff;border: 1px solid #ccc'>" + column.ColumnName +
                          "</th>");
            }

            sb.Append("</tr>");


            //Adding DataRow.
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<td style='width:100px;border: 1px solid #ccc;text-align: center'>" + row[column.ColumnName].ToString() +
                              "</td>");
                }

                sb.Append("</tr>");
            }

            //Table end.
            sb.Append("</table>");
            return sb;
        }
    }
}