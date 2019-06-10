using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Dal.Memory
{
    public class AdminMemory : IAanpassenGegevensUser
    {
        DbConn db = new DbConn("Data Source=mssql.fhict.local;User ID=dbi410994_limbofun;Password=mtbRqAp9rB3L27bfcW5g;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public void EditUser(UserIngame user)
        {
            throw new NotImplementedException();
        }

        public void IsAdmin(Admin admin)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin2(int userid)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    try
                    {
                        return GetAdminInfo2(userid);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return false;
            }
        }
        private bool GetAdminInfo2(int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select * from Admin where User_id= @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int r_id = (int)reader["user_id"];
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return false;
            }
        }
        public List<UserIngame> KrijgAlleUsers()
        {
            throw new NotImplementedException();
        }

        public List<UserIngame> KrijgAlleUsersItems()
        {
            throw new NotImplementedException();
        }

        public void VerwijderUser(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
