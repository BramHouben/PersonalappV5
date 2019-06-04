using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Web.Http;

namespace Dal
{
    class DbConn
    {
        private SqlConnection conn = new SqlConnection();

        public DbConn()
        {
            conn.ConnectionString = "Data Source=DESKTOP-11PH9I5;Initial Catalog=Killerapp;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //conn = new SqlConnection(conn.ConnectionString);
        }

        public SqlConnection returnconn()
        {
            return conn;
        }

        public bool checkconn()
        {
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
                return false;
            }
            return checkconn();
        }

        public void openconn()
        {
            try
            {
                conn.Open();
            }
            catch (Exception fout)
            {
                Console.WriteLine(fout.Message);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }
        public void closeconn()
        {
            conn.Close();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
