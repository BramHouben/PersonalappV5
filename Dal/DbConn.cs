using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Web.Http;

namespace Dal
{
   public class DbConn
    {
        //private SqlConnection conn = new SqlConnection();

        public DbConn(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
            //conn.ConnectionString = "Data Source=DESKTOP-11PH9I5;Initial Catalog=Killerapp;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //conn = new SqlConnection(conn.ConnectionString);
        }
        internal SqlConnection SqlConnection { get; }

        public SqlConnection returnconn()
        {
            return SqlConnection;
        }

        public bool checkconn()
        {
            if (SqlConnection != null && SqlConnection.State == ConnectionState.Closed)
            {
                SqlConnection.Open();
                return false;
            }
            return checkconn();
        }

        public void openconn()
        {
            try
            {
                SqlConnection.Open();
            }
            catch (Exception fout)
            {
                Console.WriteLine(fout.Message);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }
        public void closeconn()
        {
            SqlConnection.Close();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
