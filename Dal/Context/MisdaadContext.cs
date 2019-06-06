using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dal.Context
{
    public class MisdaadContext : IMisdaad
    {
        //private DbConn db = new DbConn();
        //private SqlConnection conn;
        private readonly DbConn db;

        public MisdaadContext(DbConn connection)
        {
            this.db = connection;
        }
        //private DbConn db = new DbConn();
        private SqlConnection conn;
        public List<Misdaad> VulListMisdaden()
        {
            List<Misdaad> misdaad = new List<Misdaad>();

            conn = db.returnconn();

            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (var command = new SqlCommand("SelecteerMisdaden", connectie)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //var reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                var item = new Misdaad();
                                item.Misdaad_id = (int)reader["misdaad_id"];
                                item.Misdaad_naam = (string)reader["misdaad_naam"];
                                item.Misdaad_beschrijving = (string)reader["misdaad_omschrijving"];
                                item.Misdaad_moeilijkheidsgraad = (int)reader["misdaad_moeilijkheidsgraad"];
                                misdaad.Add(item);
                            }
                        }
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
            return misdaad;
        }

        public void ZetInDatabase(int id, int user_id)
        {
            conn = db.returnconn();
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("insert into UserMisdaadGeschiedenis values(@misdaad, @user_id, @tijd)",connectie))
                    {
                     
                        command.Parameters.Add(new SqlParameter("user_id", user_id));
                        command.Parameters.Add(new SqlParameter("misdaad", id));
                        command.Parameters.Add(new SqlParameter("tijd", DateTime.Now));
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public void GeefReward(int id, int user_id)
        {
            conn = db.returnconn();
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();

                    var UserGeld = connectie.CreateCommand();
                    UserGeld.CommandText = "SELECT user_geld FROM UserGegevens WHERE user_id = '" + user_id + "'";
                    var ResultGeld = UserGeld.ExecuteScalar();

                    var UserXp = connectie.CreateCommand();
                    UserXp.CommandText = "SELECT user_xp FROM UserGegevens WHERE user_id = '" + user_id + "'";
                    var ResultXp = UserXp.ExecuteScalar();

                    var KrijgMisdaad_info = connectie.CreateCommand();
                    KrijgMisdaad_info.CommandText = "SELECT reward_id FROM Misdaad WHERE misdaad_id = '" + id + "'";
                    var MisdaadResult = KrijgMisdaad_info.ExecuteScalar();

                    int geld = (int)ResultGeld + (int)MisdaadResult * 100;
                    int xp = (int)ResultXp + (int)MisdaadResult * 10;

                    using (SqlCommand command = new SqlCommand("Update UserGegevens set user_xp =@xp, user_geld= @geld where user_id = @user_id",connectie))
                    {
                      
                        command.Parameters.Add(new SqlParameter("user_id", user_id));
                        command.Parameters.Add(new SqlParameter("xp", xp));
                        command.Parameters.Add(new SqlParameter("geld", geld));
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public void ZetInGevangenis(int id, int user_id)
        {
            conn = db.returnconn();
            //string Connstring = db.returnconn().ToString();
            using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
            {
                DateTime tijdnu = DateTime.Now;
                DateTime TijdGevangen = tijdnu.AddMinutes(30);

                connectie.Open();

                using (SqlCommand command = new SqlCommand("Insert into Gevangenis Values (@tijd_gevangen, @Uid, @borg, @id)",connectie))
                {
             
                    command.Parameters.Add(new SqlParameter("tijd_gevangen", TijdGevangen));
                    command.Parameters.Add(new SqlParameter("Uid", user_id));
                    command.Parameters.Add(new SqlParameter("borg", 500));
                    command.Parameters.Add(new SqlParameter("id", id));
                    command.ExecuteNonQuery();
                }
            }
        }

        public int MisdaadPlegen(int id)
        {
            try
            {
                conn = db.returnconn();
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("select misdaad_moeilijkheidsgraad from Misdaad where misdaad_id=  @id",connectie))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        id = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout);
            }

            return id;
        }
    }
}