using System;
using System.Collections.Generic;
using System.Web;

namespace ZPP___frontend.Content
{
    public class WrongMarkException : Exception { }
    public class Mark
    {
        public string name { get; set; }
        public int value { get; set; }
        public Mark(string key, int value)
        {
            name = key;
            this.value = value;
        }
        public static int Convert(string val)
        {
            int ret;
            switch (val)
            {
                case "5!": ret = 11; break;
                case "5": ret = 10; break;
                case "4,5": ret = 9; break;
                case "4": ret = 8; break;
                case "3,5": ret = 7; break;
                case "3": ret = 6; break;
                case "2": ret = 4; break;
                default: throw new WrongMarkException();
            }
            return ret;
        }
        public static Dictionary<string, string> nameMap = new Dictionary<string, string>
        {
            {"Ocena1", "1000-211bAM1"},
            {"Ocena2", "1000-212bAM2"},
            {"Ocena3", "1000-214bIOP"},
            {"Ocena4", "1000-211bWPI"},
            {"Ocena5", "1000-212bPO"},
            {"Ocena6", "1000-213bSOP"},
            {"Ocena7", "1000-213bBAD"},
            {"Ocena8", "1000-213bASD"},
            {"Ocena9", "1000-214bWWW"},
            {"Ocena10", "1000-214bSIK"}
        };
    }
}