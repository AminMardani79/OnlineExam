using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.WorkBookViewModel;
using DinkToPdf;
using DinkToPdf.Contracts;
using FSharp.Data.Runtime.BaseTypes;

namespace Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IConverter _converter;

        public ReportService(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] GeneratePdfReport(Tuple<List<WorkBookViewModel>, WorkBookInfoViewModel> models)
        {
            StringBuilder table = CreateTable.HtmlTable(models.Item1);
            var html = $@"
   <!DOCTYPE html>
   <html lang=""en"">
  <body>

  <div style='text-align: center;width: 100%;'>
            <h1>کارنامه آزمون آنلاین </h1>
            <h3>{models.Item2.TestTitle}</h3>
        </div>
        <div style='border: 1px solid #777777;border-radius: 5px;background-color: #cee2ff;box-shadow: 0 0 1px #cee2ff;width: 100%;'>
            <ul style='list-style:none;padding:12px;text-align: center;direction: rtl;color: #000;font-weight: 900;display:flex'>
                <li style='padding:10px;display:inline'>نام داوطلب : {models.Item2.StudentName}</li>
                <li style='padding:10px;display:inline'>پایه تحصیلی : {models.Item2.GradeName}</li>
                <li style='padding:10px;display:inline'>تاریخ شرکت در آزمون : {models.Item2.TestDayTime}</li>
                <li style='padding:10px;display:inline'>تعداد شرکت کننده : {models.Item2.StudentCounts}</li>
            </ul>
        </div>
        <div style='width: 100%;margin-top: 20px;'>
            <div style='text-align: center;border: 1px solid #777777;border-radius: 5px 5px 0 0;background-color: #cee2ff;box-shadow: 0 0 1px #cee2ff;border-bottom:none;padding-bottom: 10px;width: 100%;'>
                <h2>کارنامه کل</h2>
            </div>
            <table style='width: 100%;text-align: center;border: 1px solid #777777;border-radius: 0 0 5px 5px;border-top: none;width: 100%;'>
                <thead style='background-color: #dae7fa;'>
                    <tr>
                        <th>بالاترین تراز</th>
                        <th>تراز کل</th>
                        <th>رتبه</th>
                        <th>نمره</th>
                        <th style='padding: 20px;'>تعداد سوال</th>
                    </tr>
                </thead>
                <tbody style='background-color: #f0f5fc;'>
                    <tr>
                        <td>
                            {models.Item2.HighestLevel}
                        </td>
                        <td>
                            {models.Item2.AverageLevel}
                        </td>
                        <td>{models.Item2.StudentRank}</td>
                        <td>{models.Item2.AveragePercent}</td>
                        <td style='padding: 20px;'>{models.Item2.QuestionsCount}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div style='margin-top:20px'>
<div style='text-align: center;border: 1px solid #777777;border-radius: 5px 5px 0 0;background-color: #cee2ff;box-shadow: 0 0 1px #cee2ff;border-bottom:none;padding-bottom: 10px;width: 100%'>
                <h2>کارنامه دروس</h2>
            </div>
{table}
        </div>

  </body>
  </html>
  ";
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;
            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            headerSettings.Line = false;
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            footerSettings.Line = false;
            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            return _converter.Convert(htmlToPdfDocument);
        }
    }
}