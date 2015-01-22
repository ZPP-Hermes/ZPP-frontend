using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ZPP___frontend.Content
{
    public class ResponseParser
    {
        public static string ParseCourseEdition(string data)
        {
            JObject res = JObject.Parse(data);
            for (int i = 3; i > 0; i++)
            {
                string[] arr = res["course_grades"].SelectMany(tr => tr.SelectTokens(i.ToString() +"value_symbol", false))
                       .Select(t => t.Value<string>())
                       .ToArray();
                if (arr.Length > 0)
                    return arr[0];
            }
            return "2";
        }
    }
}