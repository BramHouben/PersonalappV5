using System;
using System.Collections.Generic;
using Dal.Repo;
using DAL.Context;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private UserSqlContext UserSqlContext = new UserSqlContext();
        private UserRepo UserRepo = new UserRepo();

        private string Krijgensalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public string Hashwachtwoord(string ww)
        {
            return BCrypt.Net.BCrypt.HashPassword(ww, Krijgensalt());
        }

        public bool Controlerenww(string ww, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(ww, hash);
        }

        public string gethash(string username)
        {
            string hash;
            hash = UserSqlContext.GetHash(username);
            return hash;
        }

        public bool InsertenUser(UserInlog User)
        {
            if (UserSqlContext.bestaatuser(User) == false)
            {
                return false;
            }
            else
            {
                UserSqlContext.InsertenUser(User);
            }
            return true;
        }

        public bool Inloggen(UserInlog User)
        {
            if (UserSqlContext.Inloggen(User.username, User.ww) == true)
            {
                return false;
            }
            else
            {
                string hash = gethash(User.username);
                if (Controlerenww(User.ww, hash) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void KijkVoorDagelijkseReward(int user_id)
        {
           if(UserRepo.DagGeleden(user_id) == true)
            {
                UserRepo.GeefRewardDagelijksInloggen(user_id);
            }
            else
            {

            }
        }

        public void Krijgendata(UserIngame IngameUser)
        {
            UserRepo.KrijgenData(IngameUser);
        }

        public void DeleteUser(int id)
        {
            UserSqlContext.DeleteUser(id);
        }

        public List<Clan> KrijgenClans(List<Clan> clanLijst)
        {
         return   UserRepo.KrijgenClans(clanLijst);
        }

        public void InvoerenClan(int clan_id, int user_id)
        {
            UserRepo.InvoerenClan(clan_id, user_id);
        }

        public List<Bericht> KrijgenBerichten(int clan_id)
        {
          return  UserRepo.KrijgenBerichten(clan_id);
        }

        public int AantalClanLeden(int clan_id)
        {
            return UserRepo.AantalClanLeden(clan_id);
        }

        public void BerichtPosten(int clan_id, int user_id, Bericht bericht)
        {
            UserRepo.BerichtPosten(clan_id, user_id, bericht);
        }
    }
}