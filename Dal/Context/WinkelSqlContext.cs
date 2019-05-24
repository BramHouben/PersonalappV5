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
                using (SqlConnection connectie = new SqlConnection(conn.ConnectionString))
                {
                    connectie.Open();

                    var UserGeld = connectie.CreateCommand();
                    UserGeld.CommandText = "SELECT user_geld FROM UserGegevens WHERE user_id = '" + user_id + "'";
                    var ResultGeld = UserGeld.ExecuteScalar();
                    var UserXp = connectie.CreateCommand();
                    UserXp.CommandText = "SELECT item_prijs FROM itemshop WHERE item_id = '" + item_id + "'";
                    var ResultXp = UserGeld.ExecuteScalar();
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }
        
    }
}
