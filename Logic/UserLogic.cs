using Dal.Repo;
using DAL.Context;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private UserSqlContext UserSqlContext = new UserSqlContext();
        private UserRepo UserRepo = new UserRepo();

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
            hash = UserSqlContext.GetHash(username);
            return hash;
        }

        public bool InsertenUser(UserInlog User)
        {
            if (UserSqlContext.bestaatuser(User) == false)
            {
                return false;
            }
            else
            {
                UserSqlContext.InsertenUser(User);
            }
            return true;
        }

        public bool Inloggen(UserInlog User)
        {
            if (UserSqlContext.Inloggen(User.username, User.ww) == true)
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
            UserRepo.KijkVoorDagelijkseReward(user_id);
        }

        public void Krijgendata(UserIngame IngameUser)
        {
            UserRepo.KrijgenData(IngameUser);
        }

        public void DeleUser(int id)
        {
            UserSqlContext.DeleteUser(id);
        }

        //public void checkregisteruser(UserInlog User)
        //{
        //    bool bestaataccount;

        //}
        //
    }
}