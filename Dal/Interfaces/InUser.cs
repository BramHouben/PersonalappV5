﻿using System.Collections.Generic;
using Models;

namespace Dal.Interfaces
{
    public interface InUser
    {
        void DeleteUser(int id);

        void InsertenUser(UserInlog User);

        bool bestaatuser(UserInlog User);


        int Krijgen_id(UserIngame User);

        void KrijgenData(UserIngame userIngame);

        bool Inloggen(string username, string ww);

        string GetHash(string username);
        void KijkVoorDagelijkseReward(int user_id);
        List<Clan> KrijgenClans(List<Clan> clanLijst);
        void InvoerenClan(int clan_id, int user_id);
        List<Bericht> KrijgenBerichten(int clan_id);
        int AantalClanLeden(int clan_id);
        void BerichtPosten(int clan_id, int user_id);
    }
}