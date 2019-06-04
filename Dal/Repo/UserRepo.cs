using Dal.Interfaces;
using DAL.Context;
using Models;
using System;
using System.Collections.Generic;

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

        public bool DagGeleden(int user_id) => inUser.DagGeleden(user_id);

        public List<Clan> KrijgenClans(List<Clan> clanLijst) => inUser.KrijgenClans(clanLijst);

        public void InvoerenClan(int clan_id, int user_id) => inUser.InvoerenClan(clan_id, user_id);

        public List<Bericht> KrijgenBerichten(int clan_id) => inUser.KrijgenBerichten(clan_id);

        public int AantalClanLeden(int clan_id) => inUser.AantalClanLeden(clan_id);

        public void BerichtPosten(int clan_id, int user_id, Bericht bericht) => inUser.BerichtPosten(clan_id, user_id, bericht);

        public void GeefRewardDagelijksInloggen(int user_id) => inUser.GeefRewardDagelijksInloggen(user_id);
    }
}