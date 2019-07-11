using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public class SqlDataAccess
    {
        private static string GetConnectionString()
        {
            string conString = "Server=127.0.0.1; Port=3306; Database=APIProject; Uid=root";

            return conString;
        }

        //public static List<T> LoadData<T>(string sql)
        //{

        //}
    }
}
