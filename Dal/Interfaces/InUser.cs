﻿using Models;

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
    }
}