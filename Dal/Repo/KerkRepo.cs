using Dal.Context;
using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repo
{
    public class KerkRepo
    {
        private readonly IKerk InKerk;
        public KerkRepo()
        {
            InKerk = new KerkContext();
        }

        public void GeefInfoVoorKerk(int user_id, Kerk kerk) => InKerk.GeefInfoVoorKerk(user_id, kerk);

        public void LevensToevoegen(int kerkid, int user_id) => InKerk.LevensToevoegen(kerkid, user_id);
    }
}
