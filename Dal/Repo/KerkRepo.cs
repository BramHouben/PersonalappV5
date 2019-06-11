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
        public KerkRepo(IKerk inkerk)
        {
            InKerk = inkerk;
        }

        public void GeefInfoVoorKerk(int user_id, Kerk kerk) => InKerk.GeefInfoVoorKerk(user_id, kerk);

        public void LevensToevoegen(int user_id) => InKerk.LevensToevoegen(user_id);

        public DateTime KrijgTijd(int user_id) => InKerk.KrijgTijd(user_id);

        public int KrijgLevensInfo(int user_id) => InKerk.KrijgLevensInfo(user_id);
    }
}
