using System;
using System.Collections.Generic;
using System.Text;
using Dal.Interfaces;
using Models;

namespace Dal.Repo
{
    public class GevangenisRepo
    {
        private IGevangenis iGevangenis;

        public GevangenisRepo(IGevangenis inGevangenis)
        {
            iGevangenis = inGevangenis;
        }

        public void KrijgGegevens(Gevangenis gevangenis) => iGevangenis.KrijgGegevens(gevangenis);

        public bool CheckUserVast(int user_id) => iGevangenis.CheckUserVast(user_id);

        public bool MagUserVrij(int user_id) => iGevangenis.MagUserVrij(user_id);

        public int CheckGeldUser(int user_id) => iGevangenis.CheckGeldUser(user_id);

        public void BetalenBorg(int bedragOver, int user_id) => iGevangenis.BetalenBorg(bedragOver, user_id);

        public int KrijgBorg(int user_id) => iGevangenis.KrijgenBorg(user_id);
    }
}
