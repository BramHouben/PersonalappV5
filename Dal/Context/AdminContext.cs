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

        public List<UserIngame> KrijgAlleUsers()
        {
            var Users = new List<UserIngame>();
            try
            {
                conn = db.returnconn();
                using (SqlConnection con = new SqlConnection(conn.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("select t1.user_id, t1.username, t1.email_user, t2.user_level, t2.user_xp, t2.user_geld, t2.user_level, t2.clan_id from UserInlog t1 inner join UserGegevens t2 on t1.user_id = t2.user_id", con)
                    )
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var User = new UserIngame
                                {
                                    user_id = (int)reader["user_id"],
                                    username = (string)reader["username"],
                                    email = (string)reader["email_user"],
                                    ingameGeld = (int)reader["user_geld"],
                                    level = (int)reader["user_level"],
                                    xp = (int)reader["user_xp"],
                                    // clan moet nog komen
                                };

                                Users.Add(User);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }

            return Users;

        }
        public void VerwijderUser(int user_id)
        {
            try
            {
                conn = db.returnconn();
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("delete from Userinlog where user_id = @user_id", connectie))
                    {

                        command.Parameters.AddWithValue("@user_id", user_id);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }

        public void EditUser(UserIngame User)
        {
            try
            {
                conn = db.returnconn();
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Update UserGegevens set user_geld = @geld ,user_leven = @leven, user_xp= @xp where user_id=@user_id", connectie))
                    {

                        command.Parameters.AddWithValue("@user_id", User.user_id);
                        command.Parameters.AddWithValue("@geld", User.ingameGeld);
                        command.Parameters.AddWithValue("@leven", User.levens);
                        command.Parameters.AddWithValue("@xp", User.xp);
                        command.ExecuteNonQuery();



                    }

                }
            }


            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }
    }
}
