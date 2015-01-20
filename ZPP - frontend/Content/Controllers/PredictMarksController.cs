using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using ZPP___frontend.Content;
//using Microsoft.AnalyssServices.AdomdClient;

namespace ZPP___frontend.Controllers
{
    public class PredictMarksController : Controller
    {
        Dictionary<int, String> _markTranslator = new Dictionary<int, String>()
        {
            {4, "2"},
            {6, "3"},
            {7, "3.5"},
            {8, "4"},
            {9, "4.5"},
            {10, "5"},
            {11, "5!"}
        };

        List<SelectListItem> _marksList = new List<SelectListItem>();

        static int _numberOfInputSubjects = 10;
        static int _numberOfPredictedSubjects = 5;


        public PredictMarksController()
        {
            ViewBag.Message = "Wpisz oceny ze swoich przedmiotów lub zaimportuj dane z usosa";

            for (int i = 1; i <= _numberOfInputSubjects; i++)
            {
                String numOfMark = "Ocena" + i.ToString();
                ViewData[numOfMark] = _marksList;
            }

            foreach (KeyValuePair<int, string> item in _markTranslator)
            {
                _marksList.Add(new SelectListItem 
                { Text = item.Value, Value = item.Key.ToString() } );
            }
        }



        public ActionResult GetMarksFromDatabase()
        {
            // TODO -> writing to items data from USOS
            return View("PredictMarks");
        }

        public ActionResult PredictMarks()
        {
            return View();
        }

        // connection with analisys services

        public ActionResult HandlePredictedMarks(int Ocena1, int Ocena2, int Ocena3, 
            int Ocena4, int Ocena5, int Ocena6, int Ocena7, int Ocena8, int Ocena9, int Ocena10)
        {
            //////////////////////////////////// connecting to database ////////////////////////

            DBConnector dbc = new DBConnector();
            SqlConnection conn = dbc.connect();
            try
            {
                conn.Open();
            }
            catch(Exception e)
            {
                ViewData["Error"] = "Database error contact administrator" + e.ToString();
                return View("Error");
            }

            ///////////////////////////////// executing prediction query on data ///////////////

            ArrayList predictedMarks = new ArrayList(new[] {4, 6, 7, 8, 11});
            try
            {
              /* TODO -> new query
              SqlDataReader myReader = null;
              SqlCommand query = new SqlCommand("SELECT * FROM OPENQUERY(DMServer, 'SELECT FLATTENED Predict ([Student].[Przedmiot], INCLUDE_STATISTICS) From [Student] NATURAL PREDICTION JOIN(SELECT (SELECT 1 AS [Przedmiot ID], "+Ocena1+" AS [Ocena] UNION SELECT 2 AS [Przedmiot ID], "+Ocena2+" AS [Ocena] UNION SELECT 3 AS [Przedmiot ID],"+Ocena3+" AS [Ocena]UNION SELECT 4 AS [Przedmiot ID],"+Ocena4+" AS [Ocena]UNION SELECT 5 AS [Przedmiot ID],"+Ocena5+" AS [Ocena]UNION SELECT 6 AS [Przedmiot ID],"+Ocena6+" AS [Ocena]UNION SELECT 7 AS [Przedmiot ID],"+Ocena7+" AS [Ocena]UNION SELECT 8 AS [Przedmiot ID],"+Ocena8+" AS [Ocena]UNION SELECT 9 AS [Przedmiot ID],"+Ocena9+" AS [Ocena]UNION SELECT 10 AS [Przedmiot ID],"+Ocena10+" AS [Ocena]) AS [Przedmiot]) AS t')", conn);
              myReader = query.ExecuteReader();
              while (myReader.Read())
              {

                  predictedMarks.Add(Convert.ToInt32(myReader[1]));
              }*/
            }
            catch (Exception e)
            {
                ViewData["Error"] = "błąd bazy danych przy analizie danych" + e.ToString();
                return View("Error");
            }

            /////////////////////////////////// displaying a data /////////////////////////////

            if (predictedMarks.Count > 0)
            {
                for (int i = 0; i < _numberOfPredictedSubjects; i++ )
                {
                    String subjectPrediction = "przedmiot" + (i+1).ToString();
                    ViewData[subjectPrediction] = _markTranslator[(int)predictedMarks[i]];
                }
                return View("PredictMarksResults");
            }
            else
            {
                ViewData["Error"] = "błąd bazy danych, za mało przedmiotów do predykcji";
                return View("Error");
            }

            
        }

    }
}