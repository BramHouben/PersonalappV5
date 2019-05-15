using Dal;
using Dal.Interfaces;
using Models;
using System;
using System.Data.SqlClient;

namespace DAL.Context
{
    public class UserSqlContext : InUser
    {
        private DbConn db = new DbConn();
        private SqlConnection conn;
        private string hash;

        public void InsertenUser(UserInlog User)
        {
            try
            {
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@username", User.username);
                command.Parameters.AddWithValue("@email", User.email);
                command.Parameters.AddWithValue("@hash_ww", User.ww);
                command.Parameters.AddWithValue("@tijd", DateTime.Now);

                command.CommandText = "insert into UserInlog (username, email_user,hash_ww,DagelijkseInlog) Values(@username, @email, @hash_ww, @tijd)";
                command.ExecuteNonQuery();
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

        public bool bestaatuser(UserInlog User)
        {
            try
            {
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@username", User.username);
                command.Parameters.AddWithValue("@email", User.email);
                command.CommandText = "Select count(*) from UserInlog where Username= @Username or email_user = @email";
                int result = (int)command.ExecuteScalar();
                if (result > 0)
                {
                    return false;
                }
                else
                {
                    return true;
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
            return bestaatuser(User);
        }

        public void DeleteUser(int id)
        {
            try
            {
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@user_id", id);
                command.CommandText = "Delete from userInlog where User_id=@user_id";
                command.ExecuteNonQuery();
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

        public int Krijgen_id(UserIngame User)
        {
            try
            {
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@username", User.username);
                command.CommandText = "Select user_id from UserInlog where username= @username";
                User.user_id = (int)command.ExecuteScalar();
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                conn.Close();
            }
            return User.user_id;
        }

        public void KrijgenData(UserIngame userIngame)
        {
            try
            {
                Krijgen_id(userIngame);
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@user_id", userIngame.user_id);
                command.CommandText = "Select *from UserGegevens where user_id= @user_id";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    userIngame.ingameGeld = (int)reader["user_geld"];
                    userIngame.level = (int)reader["user_leven"];
                    userIngame.xp = (int)reader["user_xp"];
                    userIngame.levens = (int)reader["user_leven"];
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

        public bool Inloggen(string username, string ww)
        {
            try
            {
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@username", username);
                command.CommandText = "Select count(*) from UserInlog where Username= @Username";
                int result = (int)command.ExecuteScalar();
                if (result > 0)
                {
                    return false;
                }
                else
                {
                    return true;
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
            return Inloggen(username, ww);
        }

        public string GetHash(string username)
        {
            try
            {
                conn = db.returnconn();
                conn.Open();
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@username", username);
                command.CommandText = "Select hash_ww from UserInlog where Username= @Username";
                hash = (string)command.ExecuteScalar();
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                conn.Close();
            }
            return hash;
        }

        public void KijkVoorDagelijkseReward(int user_id)
        {
            try
            {
                if (DagGeleden(user_id) == true)
                {
                    GeefRewardDagelijksInloggen(user_id);
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }

        private void GeefRewardDagelijksInloggen(int id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();

                    var UserGeld = connectie.CreateCommand();
                    UserGeld.CommandText = "SELECT user_geld FROM UserGegevens WHERE user_id = '" + id + "'";
                    var ResultGeld = UserGeld.ExecuteScalar();
                    var UserXp = connectie.CreateCommand();
                    UserXp.CommandText = "SELECT user_xp FROM UserGegevens WHERE user_id = '" + id + "'";
                    var ResultXp = UserGeld.ExecuteScalar();

                    int geld = (int)ResultGeld + 100;
                    int xp = (int)ResultXp + 50;

                    using (SqlCommand command = new SqlCommand("Update UserGegevens set user_xp =@xp, user_geld= @geld where user_id = @user_id", connectie))
                    {
                        command.Connection = connectie;
                        command.Parameters.Add(new SqlParameter("user_id", id));
                        command.Parameters.Add(new SqlParameter("xp", xp));
                        command.Parameters.Add(new SqlParameter("geld", geld));

                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command2 = new SqlCommand("Update Userinlog set DagelijkseInlog =@tijd where user_id = @user_id", connectie))
                    {
                        command2.Parameters.Add(new SqlParameter("user_id", id));
                        command2.Parameters.Add(new SqlParameter("tijd", DateTime.Now));
                        command2.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        }

        private bool DagGeleden(int user_id)
        {
            DateTime tijd = DateTime.Now;
            DateTime TijdDagGeleden;
            double tijdverschil;
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select DagelijkseInlog from UserInlog where User_id= @id", connectie))
                    {
                        command.Parameters.AddWithValue("@tijd", tijd);
                        command.Parameters.AddWithValue("@id", user_id);

                        TijdDagGeleden = (DateTime)command.ExecuteScalar();
                        tijdverschil = (tijd - TijdDagGeleden).TotalHours;
                        if (tijdverschil >= 24)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            return DagGeleden(user_id);
        }
    }
}