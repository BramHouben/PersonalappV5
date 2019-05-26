using Dal.Interfaces;
using DAL.Context;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repo
{
   public class UserRepo
    {
        private readonly InUser inUser;
        public UserRepo()
        {
            inUser = new UserSqlContext();
        }
        
        public void KrijgenData(UserIngame User) => inUser.KrijgenData(User);

        public void KijkVoorDagelijkseReward(int user_id) => inUser.KijkVoorDagelijkseReward(user_id);

        public List<Clan> KrijgenClans(List<Clan> clanLijst) => inUser.KrijgenClans(clanLijst);

        public void InvoerenClan(int clan_id, int user_id) => inUser.InvoerenClan(clan_id, user_id);
    }
}
