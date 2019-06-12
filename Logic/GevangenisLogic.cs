using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dal.Context;
using Dal.Interfaces;
using Dal.Repo;

namespace Logic
{
  public  class GevangenisLogic
    {
        GevangenisRepo gevangenisRepo;
        //GevangenisContext GevangenisContext = new GevangenisContext();
        private IGevangenis InGevangenis;

        public GevangenisLogic(IGevangenis inGevangenis)
        {
            InGevangenis =inGevangenis;
        }

        public void KrijgenGegevens(Gevangenis gevangenis)
        {
            InGevangenis.KrijgGegevens(gevangenis);
        }

        public bool BetalenBorg(int user_id/*, int borg*/)
        {
          int borg= InGevangenis.KrijgenBorg(user_id);


            int geld = InGevangenis.CheckGeldUser(user_id);
            int BedragOver = geld - borg;
            if(geld >= borg)
            {
                InGevangenis.BetalenBorg(BedragOver ,user_id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MagUserVrij(int user_id)
        {
            return InGevangenis.MagUserVrij(user_id);
        }

        public bool CheckUserVast(int user_id)
        {
          return InGevangenis.CheckUserVast(user_id);
        }

        public bool GenoegLevens(int user_id)
        {

            int levens = InGevangenis.GenoegLevens(user_id);
            if (levens <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        //public List<Gevangenis> VullenLijst()
        //{

        //    return GevangenisContext.VulLijststMisdaden();
        //}
    }
}
