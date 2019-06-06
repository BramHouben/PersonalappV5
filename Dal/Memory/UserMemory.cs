using Dal.Interfaces;
using DAL.Context;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool Inloggen(string username, string ww)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
