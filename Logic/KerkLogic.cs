using System;
using System.Collections.Generic;
using System.Text;
using Dal.Repo;
using Models;

namespace Logic
{
    public class KerkLogic
    {
        private KerkRepo KerkRepo = new KerkRepo();
        public void GetInfoVoorKerk(int user_id, Kerk kerk)
        {
            KerkRepo.GeefInfoVoorKerk(user_id,kerk);
        }
    }
}
