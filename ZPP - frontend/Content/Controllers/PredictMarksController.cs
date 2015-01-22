using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.Data.OleDb;
using ZPP___frontend.Content;
using ZPP___frontend.Models;
//using Microsoft.AnalyssServices.AdomdClient;

namespace ZPP___frontend.Controllers
{
    public class PredictMarksController : Controller
    {
        public static Dictionary<int, String> _markTranslator = Subjects._markTranslator;
        public static Dictionary<String, String> _subjectTranslator = Subjects._subjectTranslator;


        List<SelectListItem> _marksList = new List<SelectListItem>();
        List<SelectListItem> _subjectList = new List<SelectListItem>();


        static int _numberOfInputSubjects = 10;
        static int _numberOfPredictedSubjects = 5;


        public PredictMarksController()
        {

            ViewBag.Message = "Wpisz oceny ze swoich przedmiotów lub zaimportuj dane z usosa";



            foreach (KeyValuePair<string, string> item in _subjectTranslator)
            {
                _subjectList.Add(new SelectListItem { Text = item.Value, Value = item.Key });
            }

            foreach (KeyValuePair<int, string> item in _markTranslator)
            {
                _marksList.Add(new SelectListItem { Text = item.Value, Value = item.Key.ToString() });
            }
        }


        public ActionResult PredictMarks()
        {
            SubjectModel Model = new SubjectModel
            {
                SelectedSubject1 = "BD",
                SelectedSubject2 = "ASD",
                SelectedSubject3 = "SI",
                SelectedMark1 = "4",
                SubjectsList = _subjectList,
                MarksList = _marksList
            };
            return View(Model);
        }


        // TODO widok do brania danych z usosa
        public ActionResult GetFromUsos()
        {
            // przedmioty w liście _selectedSubjects, oceny wpisać do _selectedMarks

            return View();
        }


        public ActionResult PredictAndShowMarks(int Ocena1, int Ocena2,
        int Ocena3, int Ocena4, int Ocena5, int OCena6, int Ocena7, int Ocena8, int Ocena9, int Ocena10)
        {

            //////////////////////////////////// connecting to database ////////////////////////

            DBConnector dbc = new DBConnector();
            SqlConnection conn = dbc.connect();
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                ViewData["Error"] = "Database error contact administrator" + e.ToString();
                return View("Error");
            }

            ///////////////////////////////// executing prediction query on data ///////////////


            ArrayList predictedMarks = new ArrayList();
            try
            {
                //
                SqlDataReader myReader = null;
                SqlCommand query = new SqlCommand("SELECT * FROM OPENQUERY(DMSERVER, 'SELECT FLATTENED Predict ([Student].[Przedmiot], INCLUDE_STATISTICS) From [Student] NATURAL PREDICTION JOIN(SELECT (SELECT 1 AS [Przedmiot ID], "+Ocena1+" AS [Ocena] UNION SELECT 2 AS [Przedmiot ID], "+Ocena2+" AS [Ocena] UNION SELECT 3 AS [Przedmiot ID],"+Ocena3+" AS [Ocena]UNION SELECT 4 AS [Przedmiot ID],"+Ocena4+" AS [Ocena]UNION SELECT 5 AS [Przedmiot ID],"+Ocena5+" AS [Ocena]UNION SELECT 6 AS [Przedmiot ID],"+Ocena6+" AS [Ocena]UNION SELECT 7 AS [Przedmiot ID],"+Ocena7+" AS [Ocena]UNION SELECT 8 AS [Przedmiot ID],"+Ocena8+" AS [Ocena]UNION SELECT 9 AS [Przedmiot ID],"+Ocena9+" AS [Ocena]UNION SELECT 10 AS [Przedmiot ID],"+Ocena10+" AS [Ocena]) AS [Przedmiot]) AS t')", conn);
                myReader = query.ExecuteReader();
                while (myReader.Read())
                {

                    predictedMarks.Add(Convert.ToInt32(myReader[1]));
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = "błąd bazy danych przy analizie danych" + e.ToString();
                return View("Error");
            }

            /////////////////////////////////// displaying a data /////////////////////////////

            if (predictedMarks.Count > 0)
            {
                for (int i = 0; i < _numberOfPredictedSubjects; i++)
                {
                    String subjectPrediction = "przedmiot" + (i + 1).ToString();
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


        private SubjectModel GetModelFromDictionary(Dictionary<String, String> data)
        {
            SubjectModel Model = new SubjectModel
            {
                SelectedSubject1 = data.Keys.ElementAt(0),
                SelectedSubject2 = data.Keys.ElementAt(1),
                SelectedSubject3 = data.Keys.ElementAt(2),
                SelectedSubject4 = data.Keys.ElementAt(3),
                SelectedSubject5 = data.Keys.ElementAt(4),
                SelectedSubject6 = data.Keys.ElementAt(5),
                SelectedSubject7 = data.Keys.ElementAt(6),
                SelectedSubject8 = data.Keys.ElementAt(7),
                SelectedSubject9 = data.Keys.ElementAt(8),
                SelectedSubject10 = data.Keys.ElementAt(9),
                SelectedMark1 = data.Values.ElementAt(0),
                SelectedMark2 = data.Values.ElementAt(1),
                SelectedMark3 = data.Values.ElementAt(2),
                SelectedMark4 = data.Values.ElementAt(3),
                SelectedMark5 = data.Values.ElementAt(4),
                SelectedMark6 = data.Values.ElementAt(5),
                SelectedMark7 = data.Values.ElementAt(6),
                SelectedMark8 = data.Values.ElementAt(7),
                SelectedMark9 = data.Values.ElementAt(8),
                SelectedMark10 = data.Values.ElementAt(9),
                SubjectsList = _subjectList,
                MarksList = _marksList
            };
            return Model;
        }

    }
}
