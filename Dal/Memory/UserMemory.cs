using Dal.Interfaces;
using DAL.Context;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Dal.Memory
{
   public class UserMemory : InUser
    {
        //SqlConnection conn;
        DbConn db = new DbConn("Data Source=mssql.fhict.local;User ID=dbi410994_limbofun;Password=mtbRqAp9rB3L27bfcW5g;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        string hash;

        //private DbConn db = new DbConn();
        //private SqlConnection conn = "";

        //UserSqlContext usersqlcontext = new UserSqlContext();

        public int AantalClanLeden(int clan_id)
        {
            throw new NotImplementedException();
        }

        public void BerichtPosten(int clan_id, int user_id)
        {
            throw new NotImplementedException();
        }

        public void BerichtPosten(int clan_id, int user_id, Bericht bericht)
        {
            throw new NotImplementedException();
        }

        public bool bestaatuser(UserInlog User)
        {
            try
            {
                //conn = db.returnconn();
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
                Console.WriteLine(fout.Message);
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
                    using (SqlCommand command = new SqlCommand("delete from UserInlog where user_id= @user_id", connectie))
                    {

                        command.Parameters.AddWithValue("@user_id", id);
             command.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException fout)
            {

                Console.WriteLine(fout.Message);

            }
        }

        public string GetHash(string username)
        {
            try
            {
                //conn = db.returnconn();
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
                Console.WriteLine(fout.Message);
            }

            return hash;
        }

        public bool Inloggen(string username, string ww)
        {
            try
            {
                //conn = db.returnconn();
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
                Console.WriteLine(fout.Message);
            }

            return Inloggen(username, ww);
        }

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
            }catch(SqlException fout)
            {
              
                Console.WriteLine(fout.Message);
                
            }
        }

        public void InvoerenClan(int clan_id, int user_id)
        {
            throw new NotImplementedException();
        }

        public void DagGeleden(int user_id)
        {
            throw new NotImplementedException();
        }

        public List<Bericht> KrijgenBerichten(int clan_id)
        {
            throw new NotImplementedException();
        }

        public List<Clan> KrijgenClans(List<Clan> clanLijst)
        {
            throw new NotImplementedException();
        }

        public void KrijgenData(UserIngame userIngame)
        {
            try
            {
                
                //conn = db.returnconn();
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
                Console.WriteLine(fout.Message);
            }
        }
        private List<Item> KijkvoorItems(UserIngame userIngame)
        {
            userIngame.itemlist = new List<Item>();
            try
            {
                //conn = db.returnconn();
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
                Console.WriteLine(fout.Message);
            }
            return userIngame.itemlist;
        }

        public int Krijgen_id(UserIngame User)
        {
            throw new NotImplementedException();
        }

        bool InUser.DagGeleden(int user_id)
        {
            throw new NotImplementedException();
        }

        public void GeefRewardDagelijksInloggen(int user_id)
        {
            throw new NotImplementedException();
        }

        public void HaalLevensEraf(int user_id, int erafhalen)
        {
            throw new NotImplementedException();
        }

        public int KrijgLevens(int user_id)
        {
            throw new NotImplementedException();
        }
        //UserInlog inlog;


        //public void InsertenUser(UserInlog inlog)
        //{
        //    inlog.email = "Unittest@test.com";
        //    inlog.username = "Unit";
        //    inlog.user_id = 0;
        //    inlog.ww = "hashunit";
        //    usersqlcontext.InsertenUser(inlog);

        //}

        //public void DeleteUser(int id)
        //{
        //                id = 0;




        //}
    }
}
