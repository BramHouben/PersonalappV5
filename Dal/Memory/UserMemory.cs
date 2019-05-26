using Dal.Interfaces;
using DAL.Context;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Memory
{
   public class UserMemory : InUser
    {
        UserSqlContext usersqlcontext = new UserSqlContext();

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
            throw new NotImplementedException();
        }

        public void InvoerenClan(int clan_id, int user_id)
        {
            throw new NotImplementedException();
        }

        public void KijkVoorDagelijkseReward(int user_id)
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
