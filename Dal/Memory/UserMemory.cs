using Dal.Interfaces;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal.Memory
{
    public class UserMemory : InUser
    {
        public List<UserInlog> userlist = new List<UserInlog>();
      public  List<Bericht> berichten = new List<Bericht>();
        public UserMemory()
        {
            userlist.Add(new UserInlog(1, "test1@test.com", "test1", "Test123!"));
            userlist.Add(new UserInlog(2, "test2@test.com", "test2", "Test123!"));
            userlist.Add(new UserInlog(3, "test3@test.com", "test3", "Test123!"));

        }

        public int AantalClanLeden(int clan_id)
        {
            throw new NotImplementedException();
        }

 
        public void BerichtPosten(int clan_id, int user_id, Bericht bericht)
        {
            if (bericht.Bericht_inhoud==""||bericht.Bericht_titel=="")
            {
                throw new ArgumentException();
            }


         
            berichten.Add(bericht);
        }

        public bool bestaatuser(UserInlog User)
        {
         
           if (userlist.Any(x => x.email == User.email || x.username == User.username))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DeleteUser(int id)
        {
            if (userlist.Any(x => x.user_id == id))
            {
                userlist.RemoveAt(id);

            }
            else
            {
                throw new ArgumentException();
            }
        }

        public string GetHash(string username)
        {
            string hash = "$2y$12$bQlO8ExWuxtLByZW9j5J.uDip8is24.7t7D1pnTENZtcQjyDXoeqK";

            return hash;

        }

        public bool Inloggen(string username, string ww)
        {
            UserInlog User = userlist.Find(item => item.username == username);

            if(User.ww== ww)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void InsertenUser(UserInlog User)
        {
            if (User.email == "" || User.username == "" || User.ww == "")
            {
                throw new ArgumentException();
            }
            else if (userlist.Any(x => x.email == User.email|| x.username==User.username))
            {
                //fout
                throw new ArgumentException();
            }
            else
            {
                userlist.Add(User);
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
            //user 1 is meer als een dag geleden voor het laatst ingelogd
            if (user_id==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GeefRewardDagelijksInloggen(int user_id)
        {
             UserIngame userIngame = new UserIngame();
            userIngame.ingameGeld += 50;
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