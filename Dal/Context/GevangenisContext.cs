using Dal.Interfaces;
using Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dal.Context
{
    public class GevangenisContext : IGevangenis
    {
        private readonly DbConn db;

        public GevangenisContext(DbConn connection)
        {
            this.db = connection;
        }

        public void KrijgGegevens(Gevangenis gevangenis)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select *from Gevangenis where user_id= @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", gevangenis.User_id);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            gevangenis.Borg = (int)reader["Borg"];
                            gevangenis.Tijd_vast = (DateTime)reader["tijd_gevangen"];
                            gevangenis.Gevangenis_id = (int)reader["gevangenis_id"];
                        }
                        else
                        {
                            Debug.WriteLine("Geen reords");
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public bool MagUserVrij(int user_id)
        {
            try
            {
                DateTime TijdNu = DateTime.Now;

                //conn = db.returnconn();
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    DateTime tijdvast;
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Select tijd_gevangen from Gevangenis where User_id = @user_id", connectie))
                    {
                        command.Parameters.Add(new SqlParameter("user_id", user_id));
                        if (command.ExecuteScalar() == null)
                        {
                            return true;
                        }
                        else
                        {
                            tijdvast = (DateTime)command.ExecuteScalar();
                            if (TijdNu > tijdvast)
                            {
                                DeleteUserGevangenis(user_id);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (SqlException error)
            {
                Debug.WriteLine(error.Message);
            }
            return false;
        }

        public bool CheckUserVast(int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Select count(@user_id) from Gevangenis where User_id = @user_id", connectie))
                    {
                        int aantal;

                        command.Parameters.Add(new SqlParameter("user_id", user_id));
                        aantal = (int)command.ExecuteScalar();
                        if (aantal == 0)
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
                Debug.WriteLine(error.Message);
            }
            return CheckUserVast(user_id);
        }

        public int CheckGeldUser(int user_id)
        {
            int geld = 0;
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Select user_geld from UserGegevens where user_id= @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@User_id", user_id);

                        geld = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return geld;
        }

        private void DeleteUserGevangenis(int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Delete from Gevangenis where user_id= @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@User_id", user_id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException error)
            {
                Debug.WriteLine(error.Message);
            }
        }

        public void BetalenBorg(int bedrag, int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("update UserGegevens set user_geld=@Geld where user_id= @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@User_id", user_id);
                        command.Parameters.AddWithValue("@Geld", bedrag);

                        command.ExecuteNonQuery();

                        DeleteUserGevangenis(user_id);
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public int KrijgenBorg(int user_id)
        {
            int borg = 0;
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("select borg from gevangenis where user_id=@user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@User_id", user_id);
                        borg = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return borg;
        }

        public int GenoegLevens(int user_id)
        {
            int levens = 0;
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Select user_leven from UserGegevens where User_id = @user_id", connectie))
                    {
                        command.Parameters.Add(new SqlParameter("user_id", user_id));
                        levens = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException error)
            {
                Debug.WriteLine(error.Message);
            }
            return levens;
        }
    }
}