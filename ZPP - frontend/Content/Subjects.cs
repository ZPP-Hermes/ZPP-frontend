using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ZPP___frontend.Content
{
    public static class Subjects
    {
        public static Dictionary<int, String> _markTranslator = new Dictionary<int, String>()
        {
            {4, "2"},
            {6, "3"},
            {7, "3.5"},
            {8, "4"},
            {9, "4.5"},
            {10, "5"},
            {11, "5!"}
        };

        public static Dictionary<String, String> _subjectTranslator = new Dictionary<string, string>()
        {
            {"MD", "Matematyka dyskretna"},
            {"ASD", "Algorytmy i struktury danych"},
            {"BD", "Bazy danych"},
            {"PMAT", "Podstawy matematyki"},
            {"WWW", "Aplikacje WWW"},
            {"IPP", "Indywidualny projekt programistyczny"},
            {"AM2", "Analiza II"},
            {"RPiS", "Rachunek prawdopodbieństwa i statystyka"},
            {"SO", "Systemy operacyjne"},
            {"AKS", "Architektura komputerów i sieci"},
            {"ALG", "Algorytmy i struktury danych"},
            {"ZBD", "Zaawansowane bazy danych"},
            {"SI", "Sztuczna inteligencja i systemy doradcze"},
            {"SUS", "Systemy uczące się"},
            {"ZSO", "Zaawansowane systemy operacyjne"}
        };

        public static Dictionary<String, String>.KeyCollection _subjects = _subjectTranslator.Keys;
    }
}