using Dal;
using Dal.Interfaces;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DAL.Context
{
    public class UserSqlContext : InUser
    {
        private readonly DbConn db;

        public UserSqlContext(DbConn connection)
        {
            this.db = connection;
        }

        private string hash;

        public void InsertenUser(UserInlog User)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("insert into UserInlog (username, email_user,hash_ww,DagelijkseInlog) Values(@username, @email, @hash_ww, @tijd)", connectie))
                    {
                        command.Parameters.AddWithValue("@username", User.username);
                        command.Parameters.AddWithValue("@email", User.email);
                        command.Parameters.AddWithValue("@hash_ww", User.ww);
                        command.Parameters.AddWithValue("@tijd", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public bool bestaatuser(UserInlog User)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select count(*) from UserInlog where Username= @Username or email_user = @email", connectie))
                    {
                        command.Parameters.AddWithValue("@username", User.username);
                        command.Parameters.AddWithValue("@email", User.email);
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
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return bestaatuser(User);
        }

        public void DeleteUser(int id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Delete from userInlog where User_id=@user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public int Krijgen_id(UserIngame User)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select user_id from UserInlog where username= @username", connectie))
                    {
                        command.Parameters.AddWithValue("@username", User.username);

                        User.user_id = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return User.user_id;
        }

        public void KrijgenData(UserIngame userIngame)
        {
            try
            {
                Krijgen_id(userIngame);

                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select *from UserGegevens where user_id= @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", userIngame.user_id);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            userIngame.ingameGeld = (int)reader["user_geld"];
                            userIngame.level = (int)reader["user_level"];
                            userIngame.xp = (int)reader["user_xp"];
                            userIngame.levens = (int)reader["user_leven"];
                            userIngame.clan_id = (int)reader["clan_id"];

                            KijkvoorItems(userIngame);
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        private List<Item> KijkvoorItems(UserIngame userIngame)
        {
            userIngame.itemlist = new List<Item>();
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("select * from UserAankopen inner join Item on UserAankopen.item_id = item.item_id and user_id = @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", userIngame.user_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var Item = new Item
                                {
                                    Item_id = (int)reader["item_id"],
                                    Item_naam = (string)reader["item_naam"],
                                    Item_reputatie = (string)reader["item_reputatie"],
                                    Item_schade = (int)reader["item_schade"],
                                    Item_Soort = (string)reader["item_soort"],
                                };
                                userIngame.itemlist.Add(Item);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return userIngame.itemlist;
        }

        public bool Inloggen(string username, string ww)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select count(*) from UserInlog where Username= @Username", connectie))
                    {
                        command.Parameters.AddWithValue("@username", username);

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
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return Inloggen(username, ww);
        }

        public string GetHash(string username)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select hash_ww from UserInlog where Username= @Username", connectie))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        hash = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return hash;
        }

        public void GeefRewardDagelijksInloggen(int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    var UserGeld = connectie.CreateCommand();
                    UserGeld.CommandText = "SELECT user_geld FROM UserGegevens WHERE user_id = '" + user_id + "'";
                    var ResultGeld = UserGeld.ExecuteScalar();
                    var UserXp = connectie.CreateCommand();
                    UserXp.CommandText = "SELECT user_xp FROM UserGegevens WHERE user_id = '" + user_id + "'";
                    var ResultXp = UserGeld.ExecuteScalar();

                    int geld = (int)ResultGeld + 100;
                    int xp = (int)ResultXp + 50;

                    using (SqlCommand command = new SqlCommand("Update UserGegevens set user_xp =@xp, user_geld= @geld where user_id = @user_id", connectie))
                    {
                        command.Connection = connectie;
                        command.Parameters.Add(new SqlParameter("user_id", user_id));
                        command.Parameters.Add(new SqlParameter("xp", xp));
                        command.Parameters.Add(new SqlParameter("geld", geld));

                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command2 = new SqlCommand("Update Userinlog set DagelijkseInlog =@tijd where user_id = @user_id", connectie))
                    {
                        command2.Parameters.Add(new SqlParameter("user_id", user_id));
                        command2.Parameters.Add(new SqlParameter("tijd", DateTime.Now));
                        command2.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException error)
            {
                Debug.WriteLine(error.Message);
            }
        }

        public bool DagGeleden(int user_id)
        {
            DateTime tijd = DateTime.Now;
            DateTime TijdDagGeleden;
            double tijdverschil;
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
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
                Debug.WriteLine(fout.Message);
            }
            return DagGeleden(user_id);
        }

        public List<Clan> KrijgenClans(List<Clan> clanLijst)
        {
            clanLijst = new List<Clan>();
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("select * from clan", connectie))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var Clan = new Clan
                                {
                                    Clan_id = (int)reader["clan_id"],
                                    Clan_naam = (string)reader["clan_naam"],
                                };
                                clanLijst.Add(Clan);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return clanLijst;
        }

        public void InvoerenClan(int clan_id, int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Update UserGegevens Set clan_id=@clan_id Where user_id = @user_id ", connectie))
                    {
                        command.Parameters.AddWithValue("@clan_id", clan_id);
                        command.Parameters.AddWithValue("@user_id", user_id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public List<Bericht> KrijgenBerichten(int clan_id)
        {
            var Berichten = new List<Bericht>();
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("select * from Berichtenbord Where clan_id=@clan_id ", connectie))
                    {
                        command.Parameters.AddWithValue("@clan_id", clan_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var Item = new Bericht
                                {
                                    Bericht_id = (int)reader["bericht_id"],
                                    Bericht_inhoud = (string)reader["bericht_omschrijving"],
                                    Bericht_tijd = (DateTime)reader["bericht_datum"],
                                    Belangrijk_bericht = (bool)reader["belangrijk_bericht"],
                                    Bericht_titel = (string)reader["Bericht_titel"],
                                };
                                Berichten.Add(Item);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return Berichten;
        }

        public int AantalClanLeden(int clan_id)
        {
            int leden = 0;
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(clan_id)FROM UserGegevens WHERE clan_id = @clan_id ", connectie))
                    {
                        command.Parameters.AddWithValue("@clan_id", clan_id);
                        leden = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return leden;
        }

        public void BerichtPosten(int clan_id, int user_id, Bericht bericht)
        {
            try
            {
                DateTime tijdnu = DateTime.Now;

                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("insert into Berichtenbord Values(@berichtTitel,@berichtInhoud,@Status,@tijd,@user_id,@clan_id) ", connectie))
                    {
                        command.Parameters.AddWithValue("@clan_id", clan_id);
                        command.Parameters.AddWithValue("@user_id", user_id);
                        command.Parameters.AddWithValue("@berichtTitel", bericht.Bericht_titel);
                        command.Parameters.AddWithValue("@berichtInhoud", bericht.Bericht_inhoud);
                        command.Parameters.AddWithValue("@Status", bericht.Belangrijk_bericht);
                        command.Parameters.AddWithValue("@tijd", tijdnu);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public void HaalLevensEraf(int user_id, int erafhalen)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand command = new SqlCommand("Update UserGegevens Set user_leven =@Levens Where user_id = @user_id ", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);
                        command.Parameters.AddWithValue("@levens", erafhalen);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public int KrijgLevens(int user_id)
        {
            int huidigelevens = 0;
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("select user_leven from UserGegevens Where user_id = @user_id ", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);

                        huidigelevens = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return huidigelevens;
        }
    }
}