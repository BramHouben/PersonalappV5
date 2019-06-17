using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Memory
{
    public class GevangenisMemory : IGevangenis
    {
     public   List<Gevangenis> lijstGevangenis = new List<Gevangenis>();
        public GevangenisMemory()
        {
            lijstGevangenis.Add(new Gevangenis(1,1,100,DateTime.Now));
        }
        public void BetalenBorg(int bedragOver, int user_id)
        {
            throw new NotImplementedException();
        }

        public int CheckGeldUser(int user_id)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserVast(int user_id)
        {
            throw new NotImplementedException();
        }

        public int GenoegLevens(int user_id)
        {
            throw new NotImplementedException();
        }

        public int KrijgenBorg(int user_id)
        {
            throw new NotImplementedException();
        }

        public void KrijgGegevens(Gevangenis gevangenis)
        {
            if (gevangenis.User_id==1)
            {
                gevangenis.Borg = 500;
                gevangenis.Gevangenis_id = 5;
            }
        }

        public bool MagUserVrij(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
