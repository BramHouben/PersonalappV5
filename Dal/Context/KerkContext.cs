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

                    using (SqlCommand command = new SqlCommand("Select  user_leven, kerk_id, kerk_tijd from Kerk inner join UserGegevens on Kerk.user_id = UserGegevens.user_id where UserGegevens.user_id=@user_id",connectie))
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
    }
}