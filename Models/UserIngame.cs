using Model;
using System.Collections.Generic;

namespace Models
{
    public class UserIngame : UserInlog
    {
        //private int user_id { get; set; }
        public int ingameGeld { get; set; }

        public int level { get; set; }
        public int xp { get; set; }
        public List<Item> itemlist { get; set; }

        public int levens { get; set; }

        public UserIngame()
        {
        }

        public UserIngame(int User_id, double IngameGeld, int Level, int Xp, List<Item> Itemlist)
        {
        }
    }
}