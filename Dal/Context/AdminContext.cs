﻿using Dal.Interfaces;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dal.Context
{
    public class AdminContext : IAanpassenGegevensUser
    {
        private readonly DbConn db;

        public AdminContext(DbConn connection)
        {
            this.db = connection;
        }

        public bool IsAdminCheck(int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    try
                    {
                        return GetAdminInfo2(user_id);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
                return false;
            }
        }

        public void IsAdmin(Admin admin)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Select * from Admin where User_id= @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", admin.user_id);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())

                            {
                                admin.admin_id = (int)reader["admin_id"];
                                //admin.user_id = (int)reader["user_id"];
                                admin.Magaanpassen = true;
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
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
                Debug.WriteLine(fout.Message);
                return false;
            }
        }

        public List<UserIngame> KrijgAlleUsers()
        {
            var Users = new List<UserIngame>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("select t1.user_id, t1.username, t1.email_user, t2.user_level, t2.user_xp, t2.user_geld, t2.user_level, t2.clan_id from UserInlog t1 inner join UserGegevens t2 on t1.user_id = t2.user_id", con)
                    )
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var User = new UserIngame
                                {
                                    user_id = (int)reader["user_id"],
                                    username = (string)reader["username"],
                                    email = (string)reader["email_user"],
                                    ingameGeld = (int)reader["user_geld"],
                                    level = (int)reader["user_level"],
                                    xp = (int)reader["user_xp"],
                                };

                                Users.Add(User);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return Users;
        }

        public void VerwijderUser(int user_id)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("delete from Userinlog where user_id = @user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public void EditUser(UserIngame User)
        {
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("Update UserGegevens set user_geld = @geld ,user_leven = @leven, user_xp= @xp where user_id=@user_id", connectie))
                    {
                        command.Parameters.AddWithValue("@user_id", User.user_id);
                        command.Parameters.AddWithValue("@geld", User.ingameGeld);
                        command.Parameters.AddWithValue("@leven", User.levens);
                        command.Parameters.AddWithValue("@xp", User.xp);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public List<UserIngame> KrijgAlleUsersItems()
        {
            var Users = new List<UserIngame>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("select t1.user_id, t1.username, t2.item_id, COUNT(t2.item_id)aantal ,t3.item_naam from UserInlog t1 left outer join UserAankopen t2 on t1.user_id = t2.user_id left outer join Item t3 on t2.item_id = t3.item_id group by t1.user_id, t1.username, t2.item_id, item_naam order by username", con)
                    )
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var User = new UserIngame
                                {
                                    user_id = (int)reader["user_id"],
                                    username = (string)reader["username"],
                                };

                                //voor alle items binnen te krijgen

                                User.itemlist = new List<Item>();
                                var item = new Item();
                                if (reader["item_id"] == DBNull.Value)
                                {
                                    // geefnull values aan de personen die geen items hebben
                                    item.Item_id = 0;
                                    item.Item_naam = DBNull.Value.ToString();
                                }
                                else
                                {
                                    item.Item_id = (int)reader["item_id"];
                                    item.Item_naam = (string)reader["item_naam"];
                                    // als user item heeft stuur het naar de lijst
                                    User.itemlist.Add(item);
                                }

                                Users.Add(User);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return Users;
        }
    }
}