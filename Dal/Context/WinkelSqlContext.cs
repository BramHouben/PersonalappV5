using Dal.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Dal.Context
{
    public class WinkelSqlContext : IWinkel
    {
        //private DbConn db = new DbConn();
        //private SqlConnection conn;
        private readonly DbConn db;

        public WinkelSqlContext(DbConn connection)
        {
            this.db = connection;
        }
        //private DbConn db = new DbConn();
        private SqlConnection conn;

        public List<Item> Itemsophalen()
        {
            //conn = db.returnconn();
            List<Item> Ilist = new List<Item>();
            try
            {
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("select * From ItemShop inner join Item On ItemShop.item_id = Item.item_id", connectie))
                    {

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var item = new Item();
                            item.Item_id = (int)reader["Item_id"];
                            item.Item_naam = (string)reader["Item_naam"];
                            item.Item_schade = (int)reader["item_schade"];
                            item.Item_beschrijving = (string)reader["item_beschrijving"];
                            item.Item_Soort = (string)reader["item_soort"];
                            item.Item_reputatie = (string)reader["item_reputatie"];
                            item.Item_prijs = (int)reader["item_prijs"];
                            item.Item_min_level = (int)reader["item_min_level"];
                            item.Vervaldatum = (DateTime)reader["vervaldatum"];
                            item.Status = (bool)reader["status"];
                            Ilist.Add(item);
                        }

                        return Ilist;
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                //lege list
                return Ilist;
            }

        }
        public bool KanItemKopen(int item_id, int user_id)
        {
            int Kosten;
            int ResultGeld;
            int NieweRekening;
            try
            {
                //conn = db.returnconn();
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT user_geld FROM UserGegevens WHERE user_id = @user_id", connectie))
                    {

                        cmd.Parameters.AddWithValue("@user_id", user_id);
                         ResultGeld = (int)cmd.ExecuteScalar();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT item_prijs FROM itemshop WHERE item_id = @item_id", connectie))
                    {

                        cmd.Parameters.AddWithValue("@item_id", item_id);
                         Kosten = (int)cmd.ExecuteScalar();
                    }
         
                   
                    

                    if(Kosten <= ResultGeld)
                    {
                        NieweRekening = ResultGeld - Kosten;
                        using (SqlCommand cmd = new SqlCommand("Update UserGegevens Set user_geld = @User_geld where user_id = @user_id", connectie))
                        {

                            cmd.Parameters.AddWithValue("@user_id", user_id);
                            cmd.Parameters.AddWithValue("@User_geld", NieweRekening);
                            cmd.ExecuteNonQuery();
                        }

                        return true;
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return false;
            }
            return false;
        }

        public void KoopItem(int item_id, int user_id)
        {
            

            try
            {
               using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    using (SqlCommand cmd = new SqlCommand("insert into UserAankopen values( @user_id, @datum, @item_id )", connectie))
                    {

                        cmd.Parameters.AddWithValue("@item_id", item_id);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@datum", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                               
                }
            }catch(SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }
    }
}
