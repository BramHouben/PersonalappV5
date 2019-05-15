using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dal.Context;
namespace Logic
{
  public  class GevangenisLogic
    {
        GevangenisContext GevangenisContext = new GevangenisContext();

        public void KrijgenGegevens(Gevangenis gevangenis)
        {
             GevangenisContext.KrijgGegevens(gevangenis);
        }

        public bool BetalenBorg(int user_id, int borg)
        {
            int geld = GevangenisContext.CheckGeldUser(user_id);
            int BedragOver = geld - borg;
            if(geld >= borg)
            {
                GevangenisContext.BetalenBorg(BedragOver ,user_id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUserVast(int user_id)
        {
          return GevangenisContext.CheckUserVast(user_id);
        }
        //public List<Gevangenis> VullenLijst()
        //{

        //    return GevangenisContext.VulLijststMisdaden();
        //}
    }
}
