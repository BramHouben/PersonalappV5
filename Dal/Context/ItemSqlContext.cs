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
            db.openconn();
            var cmd = new SqlCommand("select * From Item", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new Item();
                item.Item_id = (int)reader["Item_id"];
                item.Item_naam = (string)reader["Item_naam"];
                item.Item_schade = (int)reader["item_schade"];
                item.Item_beschrijving = (string)reader["item_beschrijving"];
                item.Item_Soort = (string)reader["item_soort"];
                item.Item_reputatie = (string)reader["item_reputatie"];
                Ilist.Add(item);
            }
            db.closeconn();
            return Ilist;


        }



    }
}
