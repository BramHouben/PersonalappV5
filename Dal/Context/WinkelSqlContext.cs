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
            conn = db.returnconn();
            List<Item> Ilist = new List<Item>();
            try
            {
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
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
            try
            {
                conn = db.returnconn();
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();
                   
                    var UserGeld = connectie.CreateCommand();
                    UserGeld.CommandText = "SELECT user_geld FROM UserGegevens WHERE user_id = '" + user_id + "'";
                    int ResultGeld = (int)UserGeld.ExecuteScalar();
                    var ItemKosten = connectie.CreateCommand();
                    ItemKosten.CommandText = "SELECT item_prijs FROM itemshop WHERE item_id = '" + item_id + "'";
                    int Kosten = (int)ItemKosten.ExecuteScalar();

                    if(Kosten <= ResultGeld)
                    {
                       return true;
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            return false;
        }

        public void KoopItem(int item_id, int user_id)
        {
            try
            {
                conn = db.returnconn();

                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
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
