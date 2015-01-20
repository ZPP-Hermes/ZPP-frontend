using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ZPP___frontend.Content.Controllers
{
    public class PredictPopularityController : Controller
    {
        ArrayList _subjectList;
        ArrayList _teacherList;

        public PredictPopularityController()
        {
            // TODO -> fill arrays
            _subjectList = new ArrayList(new[] { "asd", "qwe" });
            _teacherList = new ArrayList(new[] { "asd", "qwe" });
        }
        
        public ActionResult PredictPopularity()
        {
            return View();
        }

        public ActionResult HandlePopularity(string subject, string teacher)
        {
            if (_subjectList.Contains(subject) && _teacherList.Contains(teacher))
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

                int predictedPopularity = 0;
                try
                {
                    /* TODO -> new query 
                    SqlDataReader myReader = null;
                    SqlCommand query = new SqlCommand("SELECT * FROMna10 + [Student]", conn);
                    myReader = query.ExecuteReader();
                    myReader.Read();
                    predictedPopularity = Convert.ToInt32(myReader[1]); */
                    ViewData["predictedPopularity"] = predictedPopularity.ToString();
                    return View("PredictPopularityResults");
                }
                catch (Exception e)
                {
                    ViewData["Error"] = "błąd bazy danych przy analizie danych" + e.ToString();
                    return View("Error");
                }
            }
            else
            {
                if (!_subjectList.Contains(subject))
                {
                    ViewData["Error"] = "nie ma takiego przedmiotu";
                }
                else
                {
                    ViewData["Error"] = "nie ma takiego prowadzącego";
                }
                return View("Error");
            }
        }
    }
}