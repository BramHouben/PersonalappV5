using Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Dal.Context
{
    public class WinkelSqlContext : IWinkel
    {
        private DbConn db = new DbConn();
        private SqlConnection conn;
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
                        KoopItem(item_id, user_id);
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
