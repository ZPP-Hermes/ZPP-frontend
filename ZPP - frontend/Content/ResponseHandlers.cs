using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

class Response
{
    [JsonProperty("course_grades")]
    public Response2 course_grades { get; set; }
}
class Response2
{
    [JsonProperty("1")]
    public RespValue p1 { get; set; }
    [JsonProperty("2")]
    public RespValue p2 { get; set; }
    [JsonProperty("3")]
    public RespValue p3 { get; set; }
}
class RespValue {
    [JsonProperty("value_symbol")]
    public string val { get; set; }
}

namespace ZPP___frontend.Content
{
    public class ResponseParser
    {
        public static string ParseCourseEdition(string data)
        {
            var ds = JsonConvert.DeserializeObject<Response>(data);
            if (ds.course_grades == null)
                return null;
            else
            {
                if (ds.course_grades.p3 != null)
                    return ds.course_grades.p3.val;
                if (ds.course_grades.p2 != null)
                    return ds.course_grades.p2.val;
                if (ds.course_grades.p1 != null)
                    return ds.course_grades.p1.val;
                return null;
            }
        }
    }
}