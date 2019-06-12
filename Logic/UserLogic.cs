using System;
using System.Collections.Generic;
using System.Net.Mail;
using Dal.Interfaces;
using DAL.Context;
using Models;

namespace Logic
{
    public class UserLogic
    {
        //private UserSqlContext UserSqlContext = new UserSqlContext();
        //private UserRepo UserRepo;
        private InUser IntUser;
        public UserLogic(InUser inUser)
        {
            IntUser= inUser;
        }
        private string Krijgensalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public string Hashwachtwoord(string ww)
        {
            return BCrypt.Net.BCrypt.HashPassword(ww, Krijgensalt());
        }

        public bool Controlerenww(string ww, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(ww, hash);
        }

        public string gethash(string username)
        {
            string hash;
            hash = IntUser.GetHash(username);
            return hash;
        }

        public bool InsertenUser(UserInlog User)
        {
            if (IntUser.bestaatuser(User) == false)
            {
                return false;
            }
            else
            {
                IntUser.InsertenUser(User);
                verstuurMail(User.email, User.username);
            }
            return true;
        }

        private void verstuurMail(string email, string Username)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("limbofunfontys@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Registratie LimboFun";
                mail.Body = "Beste "+Username+" Bedankt voor het registreren bij LimboFun!";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("limbofunfontys@gmail.com", "Fontys123!");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }catch(Exception fout)
            {
               Console.WriteLine(fout.Message);
            }
        }

        public bool Inloggen(UserInlog User)
        {
            if (IntUser.Inloggen(User.username, User.ww) == true)
            {
                return false;
            }
            else
            {
                string hash = gethash(User.username);
                if (Controlerenww(User.ww, hash) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void KijkVoorDagelijkseReward(int user_id)
        {
           if(IntUser.DagGeleden(user_id) == true)
            {
                IntUser.GeefRewardDagelijksInloggen(user_id);
            }
            else
            {
                //niks
            }
        }

        public void HaalLevensEraf(int user_id)
        {
            int Levens = IntUser.KrijgLevens(user_id);
            int Erafhalen = 10;
            if (Levens > 1)
            {
                Levens -= Erafhalen;
                if(Levens <= 0)
                {
                    Levens = 1;
                }
                IntUser.HaalLevensEraf(user_id, Levens);
            }
            
        }

        public void Krijgendata(UserIngame IngameUser)
        {
            IntUser.KrijgenData(IngameUser);
        }

        public void DeleteUser(int id)
        {
            IntUser.DeleteUser(id);
        }

        public List<Clan> KrijgenClans(List<Clan> clanLijst)
        {
         return IntUser.KrijgenClans(clanLijst);
        }

        public void InvoerenClan(int clan_id, int user_id)
        {
            IntUser.InvoerenClan(clan_id, user_id);
        }

        public List<Bericht> KrijgenBerichten(int clan_id)
        {
          return IntUser.KrijgenBerichten(clan_id);
        }

        public int AantalClanLeden(int clan_id)
        {
            return IntUser.AantalClanLeden(clan_id);
        }

        public void BerichtPosten(int clan_id, int user_id, Bericht bericht)
        {
            IntUser.BerichtPosten(clan_id, user_id, bericht);
        }
    }
}