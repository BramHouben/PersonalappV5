using Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserIngame : UserInlog
    {
        //private int user_id { get; set; }
        [Display( Name = "Geld")]
        public int ingameGeld { get; set; }

        public int level { get; set; }
        public int xp { get; set; }

        public List<Item> itemlist { get; set; }
        
        public int levens { get; set; }
        public int clan_id { get; set; }

        public UserIngame()
        {
        }

        public UserIngame(int User_id, double IngameGeld, int Level, int Xp, List<Item> Itemlist, int clan_id)
        {
        }
    }
}