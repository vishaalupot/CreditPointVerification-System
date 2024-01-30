using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using CPV_Mark3.Models;
using System.Reflection;

namespace CPV_Mark3.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }


        public static List<string> GetFieldNames<T>() where T : class
        {
            List<string> fieldNames = new List<string>();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                fieldNames.Add(property.Name);
            }

            return fieldNames;
        }

        public ActionResult DownloadCases()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            HttpResponseBase Response = HttpContext.Response;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<CaseTable> caseTables = db.CaseTables.ToList();

            var header =  GetFieldNames<CaseTable>();
               // new List<string>() { "Application_name", "Application_no", "Company_Name" };
            using (var package = new ExcelPackage())
            {
                int col = 1;
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                foreach (var Headername in header)
                {
                    worksheet.Cells[1, col++].Value = Headername.ToString();
                }

                int row = 2;
                foreach (var itemcase in caseTables)
                {
                    col = 1;
                    foreach (var Headname in header)
                    {
                        var property = typeof(CaseTable).GetProperty(Headname, BindingFlags.Public | BindingFlags.Instance);
                        worksheet.Cells[row, col++].Value = property.GetValue(itemcase); ;
                    }
                    row++;
                }

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Caselist" + DateTime.Today.ToString("dd-MM") + ".xlsx");



                // Write Excel file to response stream
                Response.BinaryWrite(package.GetAsByteArray());

                //Response.End();
                Response.Flush();

                // End the response
                Response.SuppressContent = true;
                HttpContext.ApplicationInstance.CompleteRequest();               
                return File(Response.OutputStream, Response.ContentType);



            }
        }
    }
}