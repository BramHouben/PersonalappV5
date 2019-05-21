using System;
using System.Collections.Generic;
using System.Text;
using Dal.Context;
using Dal.Interfaces;
using Models;

namespace Dal.Repo
{
    public class MisdaadRepo
    {
        private readonly IMisdaad iMisdaad;
        public MisdaadRepo()
        {
            iMisdaad = new MisdaadContext();
        }
        public List<Misdaad> VulListMisdaden() => iMisdaad.VulListMisdaden();

        public int MisdaadPlegen(int id) => iMisdaad.MisdaadPlegen(id);

        public void ZetInGevangenis(int id, int user_id) => iMisdaad.ZetInGevangenis(id, user_id);
        public void GeefReward(int id, int user_id) => iMisdaad.GeefReward(id, user_id);

        public void ZetInDatabase(int id, int user_id) => iMisdaad.ZetInDatabase(id, user_id);
    }
}
