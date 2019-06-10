using System;
using System.Collections.Generic;
using System.Net.Mail;
using Dal.Interfaces;
using Dal.Repo;
using DAL.Context;
using Models;

namespace Logic
{
    public class UserLogic
    {
        //private UserSqlContext UserSqlContext = new UserSqlContext();
        private UserRepo UserRepo;

        public UserLogic(InUser inUser)
        {
            UserRepo = new UserRepo(inUser);
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
            hash = UserRepo.GetHash(username);
            return hash;
        }

        public bool InsertenUser(UserInlog User)
        {
            if (UserRepo.bestaatuser(User) == false)
            {
                return false;
            }
            else
            {
                UserRepo.InsertenUser(User);
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
            if (UserRepo.Inloggen(User.username, User.ww) == true)
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
           if(UserRepo.DagGeleden(user_id) == true)
            {
                UserRepo.GeefRewardDagelijksInloggen(user_id);
            }
            else
            {

            }
        }

        public void Krijgendata(UserIngame IngameUser)
        {
            UserRepo.KrijgenData(IngameUser);
        }

        public void DeleteUser(int id)
        {
            UserRepo.DeleteUser(id);
        }

        public List<Clan> KrijgenClans(List<Clan> clanLijst)
        {
         return   UserRepo.KrijgenClans(clanLijst);
        }

        public void InvoerenClan(int clan_id, int user_id)
        {
            UserRepo.InvoerenClan(clan_id, user_id);
        }

        public List<Bericht> KrijgenBerichten(int clan_id)
        {
          return  UserRepo.KrijgenBerichten(clan_id);
        }

        public int AantalClanLeden(int clan_id)
        {
            return UserRepo.AantalClanLeden(clan_id);
        }

        public void BerichtPosten(int clan_id, int user_id, Bericht bericht)
        {
            UserRepo.BerichtPosten(clan_id, user_id, bericht);
        }
    }
}