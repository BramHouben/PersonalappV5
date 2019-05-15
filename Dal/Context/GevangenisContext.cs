using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Dal.Context
{
    public class GevangenisContext
    {
        private DbConn db = new DbConn();
        private SqlConnection conn;

        public void KrijgGegevens(Gevangenis gevangenis)
        {
            try
            {
               
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@user_id", gevangenis.User_id);
                command.CommandText = "Select *from Gevangenis where user_id= @user_id";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    gevangenis.Borg = (int)reader["Borg"];
                    gevangenis.Tijd_vast = (DateTime)reader["tijd_gevangen"];
                    gevangenis.Gevangenis_id = (int)reader["gevangenis_id"];


                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool CheckUserVast(int user_id)
        {
            try
            {
              conn=  db.returnconn();
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {

                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Select count(@user_id) from Gevangenis where User_id = @user_id"))
                    {
                        int aantal;
                        command.Connection = connectie;
                        command.Parameters.Add(new SqlParameter("user_id", user_id));
                      aantal=  (int)command.ExecuteScalar();
                        if(aantal == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
            return CheckUserVast(user_id);
        }

        public int CheckGeldUser(int user_id)
        {
            int geld= 0;
            try
            {
                
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@User_id", user_id);
                command.CommandText = "Select user_geld from UserGegevens where user_id= @user_id";
                geld=   (int)command.ExecuteScalar();
              
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                conn.Close();

            }
            return geld;
        }

        public void BetalenBorg(int bedrag, int user_id)
        {
            try
            {

                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@User_id", user_id);
                command.Parameters.AddWithValue("@Geld", bedrag);
                command.CommandText = "update UserGegevens set user_geld=@Geld where user_id= @user_id";
                command.ExecuteNonQuery();

                var command2 = conn.CreateCommand();
                command2.Parameters.AddWithValue("@User_id", user_id);
                command2.CommandText = "Delete from Gevangenis where user_id= @user_id";
                command2.ExecuteNonQuery();
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        //public List<Gevangenis> VulLijststMisdaden()
        //{
        //    List<Gevangenis> Gevangenis = new List<Gevangenis>();
        //    try
        //    {
        //        conn = db.returnconn();
        //        conn.Open();


        //        var cmd = new SqlCommand("select * From Gevangenis", conn);
        //        var reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var item = new Gevangenis();
        //            item.Gevangenis_id = (int)reader["Gevangenis_id"];
        //            item.Borg = (int)reader["Borg"];
        //            item.Tijd_vast = (DateTime)reader["tijd_gevangen"];

        //            Gevangenis.Add(item);

        //        }


        //    }

        //    catch (SqlException fout)
        //    {
        //        Console.WriteLine(fout);

        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return Gevangenis;
        //}

    }
}
