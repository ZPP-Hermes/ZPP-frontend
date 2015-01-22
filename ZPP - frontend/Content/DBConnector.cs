using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZPP___frontend.Content
{
    public class DBConnector
    {
        public SqlConnection connect()
        {
            string connection = "Data Source=zpptestvm.cloudapp.net;Database=Student;" +
            "User ID=admin;Password=admin;";
            SqlConnection conn = new SqlConnection(connection);
            return conn;
        }
        
    }

}