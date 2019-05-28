using Dal.Interfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Dal.Context
{
    public class ItemSqlContext : InItem
    {
        DbConn db = new DbConn();
        private SqlConnection conn = new SqlConnection();
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
            }catch(SqlException fout)
            {
                Console.WriteLine(fout.Message);
                //lege list
                return Ilist;
            }

        }



    }
}
