using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Dal.Context
{
    class AdminContext : IAanpassenGegevensUser
    {
        private DbConn db = new DbConn();
        private SqlConnection conn;

        //public bool IsAdmin(int user_id, Admin admin)
        //{
        //    conn = db.returnconn();
        //    int aantal;
        //    try
        //    {
        //        using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
        //        {
        //            connectie.Open();
        //            using (SqlCommand command = new SqlCommand("Select count(*) from Admin where User_id= @user_id", connectie))
        //            {

        //                command.Parameters.AddWithValue("@user_id", user_id);
        //                aantal = (int)command.ExecuteScalar();
        //                if (aantal >= 1)
        //                {
        //                    GetAdminInfo(user_id, admin);
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException fout)
        //    {
        //        Console.WriteLine(fout.Message);
        //    }
        //    return IsAdmin(user_id, admin);
        //}

        public bool IsAdmin2(int user_id)
        {
            conn = db.returnconn();
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    try
                    {
                        return GetAdminInfo2(user_id);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return false;
            }
        }

        public void IsAdmin(Admin admin)
        {
            conn = db.returnconn();
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select * from Admin where User_id= @user_id", connectie))
                    {

                        command.Parameters.AddWithValue("@user_id", admin.user_id);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {


                            while (reader.Read())


                            {
                                admin.admin_id = (int)reader["admin_id"];
                                //admin.user_id = (int)reader["user_id"];
                                admin.Magaanpassen = true;
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }

        private bool GetAdminInfo2(int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select * from Admin where User_id= @user_id", connectie))
                    {

                        command.Parameters.AddWithValue("@user_id", user_id);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int r_id = (int)reader["user_id"];
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return false;
            }
        }
    }
}
