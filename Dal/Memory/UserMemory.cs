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
        //DbConn db = new DbConn("Data Source=mssql.fhict.local;User ID=dbi410994_limbofun;Password=mtbRqAp9rB3L27bfcW5g;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
       

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
            throw new NotImplementedException();

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
            List<UserInlog> userlist = new List<UserInlog>();
            throw new NotImplementedException();

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
           }
        private List<Item> KijkvoorItems(UserIngame userIngame)
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
