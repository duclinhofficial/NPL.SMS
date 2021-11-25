using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPL.SMS
{
    public class Connect
    {
        // Tao chuoi ket noi
        private const string connString = "Data Source=DESKTOP-MGA8H1J\\LINHHOCTAP;Initial Catalog=SMS;User ID=sa;Password=hoctap123";

        //Tao doi tuong Sql Connection
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }

        //Tao doi tuong Sql Command
        public static SqlCommand GetSqlCommand(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            return cmd;
        }
    }
}
