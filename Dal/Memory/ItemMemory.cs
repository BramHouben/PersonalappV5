using Dal.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Dal.Context
{
    public class ItemMemory : IWinkel
    {
        private DbConn db = new DbConn("Data Source=mssql.fhict.local;User ID=dbi410994_limbofun;Password=mtbRqAp9rB3L27bfcW5g;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private static List<Item> items = new List<Item>();

        //public ItemMemory()
        //{
        //    if (items.Count == 0)
        //    {
        //        items.Add(new Item(1, "Geweer"));
        //        items.Add(new Item(2, "vork"));
        //    }
        //}

        public List<Item> Itemsophalen()
        {
            return items;
        }

        public bool KanItemKopen(int item_id, int user_id)
        {
            try
            {
                //conn = db.returnconn();
                using (SqlConnection connectie = new SqlConnection(db.SqlConnection.ConnectionString))
                {
                    connectie.Open();

                    var UserGeld = connectie.CreateCommand();
                    UserGeld.CommandText = "SELECT user_geld FROM UserGegevens WHERE user_id = '" + user_id + "'";
                    int ResultGeld = (int)UserGeld.ExecuteScalar();
                    var ItemKosten = connectie.CreateCommand();
                    ItemKosten.CommandText = "SELECT item_prijs FROM itemshop WHERE item_id = '" + item_id + "'";
                    int Kosten = (int)ItemKosten.ExecuteScalar();

                    if (Kosten <= ResultGeld)
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
                //conn = db.returnconn();

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
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }
    }
}