using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
//using Microsoft.AnalisysServices.AdomdClient;

namespace ZPP___frontend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Witamy w systemie HERMES!";

           

            return View();
        }

        public ActionResult PredictMarks()
        {
            ViewBag.Message = "Wpisz oceny ze swoich przedmiotów";

            List<SelectListItem> marks = new List<SelectListItem>();

            marks.Add(new SelectListItem { Text = "NK", Value = "4" });

            marks.Add(new SelectListItem { Text = "3", Value = "6" });

            marks.Add(new SelectListItem { Text = "3.5", Value = "7" });

            marks.Add(new SelectListItem { Text = "4", Value = "8" });

            marks.Add(new SelectListItem { Text = "4.5", Value = "9" });

            marks.Add(new SelectListItem { Text = "5", Value = "10" });

            marks.Add(new SelectListItem { Text = "5!", Value = "11" });

            ViewBag.Ocena1 = marks;
            ViewBag.Ocena2 = marks;
            ViewBag.Ocena3 = marks;
            ViewBag.Ocena4 = marks;
            ViewBag.Ocena5 = marks;
            ViewBag.Ocena6 = marks;
            ViewBag.Ocena7 = marks;
            ViewBag.Ocena8 = marks;
            ViewBag.Ocena9 = marks;
            ViewBag.Ocena10 = marks;

            return View();
        }

        // connection with analisys services

        public ActionResult HandlePredictedMarks(int Ocena1, int Ocena2, int Ocena3, 
            int Ocena4, int Ocena5, int Ocena6, int Ocena7, int Ocena8, int Ocena9, int Ocena10)
        {
            string connection = "Data Source=zpptestvm.cloudapp.net;Database=Test2;" +
            "User ID=admin;Password=admin;";
            SqlConnection conn = new SqlConnection(connection);
            ArrayList predictedMarks = new ArrayList();
            try
            {
              conn.Open();
            }
            catch(Exception)
            {
              ViewData["error"] = "Database error contact administrator";
                return View("Error");
            }
            try
            {
              SqlDataReader myReader = null;
              SqlCommand query = new SqlCommand("SELECT * FROM OPENQUERY(DMServer, 'SELECT FLATTENED Predict ([Student].[Przedmiot], INCLUDE_STATISTICS) From [Student] NATURAL PREDICTION JOIN(SELECT (SELECT 1 AS [Przedmiot ID], "+Ocena1+" AS [Ocena] UNION SELECT 2 AS [Przedmiot ID], "+Ocena2+" AS [Ocena] UNION SELECT 3 AS [Przedmiot ID],"+Ocena3+" AS [Ocena]UNION SELECT 4 AS [Przedmiot ID],"+Ocena4+" AS [Ocena]UNION SELECT 5 AS [Przedmiot ID],"+Ocena5+" AS [Ocena]UNION SELECT 6 AS [Przedmiot ID],"+Ocena6+" AS [Ocena]UNION SELECT 7 AS [Przedmiot ID],"+Ocena7+" AS [Ocena]UNION SELECT 8 AS [Przedmiot ID],"+Ocena8+" AS [Ocena]UNION SELECT 9 AS [Przedmiot ID],"+Ocena9+" AS [Ocena]UNION SELECT 10 AS [Przedmiot ID],"+Ocena10+" AS [Ocena]) AS [Przedmiot]) AS t')", conn);
              myReader = query.ExecuteReader();
              while (myReader.Read())
              {

                  predictedMarks.Add(Convert.ToInt32(myReader[1]));
              }
            }
            catch (Exception e)
            {
                ViewData["error"] = e.ToString();
                return View("Error");
            }


            if (predictedMarks.Count > 0)
            {
                ViewData["przedmiot1"] = predictedMarks[0];
                ViewData["przedmiot2"] = predictedMarks[1];
                ViewData["przedmiot3"] = predictedMarks[2];
                ViewData["przedmiot4"] = predictedMarks[3];
                ViewData["przedmiot5"] = predictedMarks[4];
                return View("PredictMarksResults");
            }
            else
            {
                ViewData["error"] = "błąd bazy danych";
                return View("Error");
            }

            
        }

        public ActionResult PredictSubjects()
        {
            ViewBag.Message = "Przewiduj przedmioty";

            return View();
        }
    }
}