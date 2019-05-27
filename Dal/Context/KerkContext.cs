using Dal.Interfaces;
using Models;
using System;
using System.Data.SqlClient;

namespace Dal.Context
{
    public class KerkContext : IKerk
    {
        private DbConn db = new DbConn();
        private SqlConnection conn;

        public void GeefInfoVoorKerk(int user_id, Kerk kerk)
        {
            conn = db.returnconn();
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Select  user_leven, kerk_id, kerk_tijd from Kerk inner join UserGegevens on Kerk.user_id = UserGegevens.user_id where UserGegevens.user_id=@user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                kerk.Kerk_id = (int)reader["kerk_id"];
                                kerk.Kerk_tijd = (DateTime)reader["kerk_tijd"];
                                kerk.User_levens = (int)reader["user_leven"];
                            }
                        }
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public DateTime KrijgTijd(int kerkid)
        {
            DateTime TijdUser = DateTime.Now;
            conn = db.returnconn();
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Select kerk_tijd from Kerk where kerk_id=@kerk_id", connectie))
                    {
                        command.Parameters.AddWithValue("@kerk_id", kerkid);
                        TijdUser = (DateTime)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
            return TijdUser;
        }

        public void LevensToevoegen(int kerkid, int user_id)
        {
            conn = db.returnconn();
            try
            {
                int user_leven;
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("select user_leven from UserGegevens where user_id=@user_id ", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);
                        user_leven = (int)command.ExecuteScalar();
                    }
                    if (user_leven >= 100)
                    {
                        using (SqlCommand command = new SqlCommand("Delete from Kerk where user_id=@user_id ", connectie))
                        {
                            command.Parameters.AddWithValue("@user_id", user_id);
                            command.ExecuteScalar();
                        }
                    }
                    else
                    {
                        using (SqlCommand command = new SqlCommand("update UserGegevens set user_leven= @levens where user_id=@user_id ", connectie))
                        {
                            command.Parameters.AddWithValue("@user_id", user_id);
                            command.Parameters.AddWithValue("@levens", user_leven + 10);
                            command.ExecuteNonQuery();
                        }
                        using (SqlCommand command = new SqlCommand("update Kerk set kerk_tijd= @Tijd where user_id=@user_id ", connectie))
                        {
                            DateTime Tijdnu = DateTime.Now;
                            DateTime tijdwachten = Tijdnu.AddMinutes(180);
                            command.Parameters.AddWithValue("@user_id", user_id);
                            command.Parameters.AddWithValue("@tijd", tijdwachten);
                            command.ExecuteNonQuery();
                        }
                    }
                    //todo update de kerktabel
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}