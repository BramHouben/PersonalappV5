using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class UserInlog
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
     
        public string ww { get; set; }

        public UserInlog()
        {
        }

        public UserInlog(int User_id, string Email, string Username, string WW)
        {
            this.user_id = User_id;
            this.email = Email;
            this.username = Username;
            this.ww = WW;
     
        }

    }
}