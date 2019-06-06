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
        

        public GevangenisLogic(IGevangenis inGevangenis)
        {
            gevangenisRepo = new GevangenisRepo(inGevangenis);
        }

        public void KrijgenGegevens(Gevangenis gevangenis)
        {
            gevangenisRepo.KrijgGegevens(gevangenis);
        }

        public bool BetalenBorg(int user_id, int borg)
        {
            int geld = gevangenisRepo.CheckGeldUser(user_id);
            int BedragOver = geld - borg;
            if(geld >= borg)
            {
                gevangenisRepo.BetalenBorg(BedragOver ,user_id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MagUserVrij(int user_id)
        {
            return gevangenisRepo.MagUserVrij(user_id);
        }

        public bool CheckUserVast(int user_id)
        {
          return gevangenisRepo.CheckUserVast(user_id);
        }
        //public List<Gevangenis> VullenLijst()
        //{

        //    return GevangenisContext.VulLijststMisdaden();
        //}
    }
}
