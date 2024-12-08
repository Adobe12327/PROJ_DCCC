using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP
{
    class Utils
    {
        public static bool IsVaildToken(long accountSeq, long token)
        {
            using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
            {
                mysql.Open();
                string selectQuery = "SELECT accessToken FROM userlist WHERE accountSeq = @accountSeq";

                var cmd = new MySqlCommand(selectQuery, mysql);
                cmd.Parameters.Add("@accountSeq", MySqlDbType.VarChar).Value = accountSeq;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if ((long)reader["accessToken"] == token)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public static int PayWithTrophy(long accountSeq, int trophy)
        {
            using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
            {
                mysql.Open();
                string query = "SELECT trophyCnt FROM userlist WHERE accountSeq = @accountSeq";

                var cmd = new MySqlCommand(query, mysql);
                cmd.Parameters.Add("@accountSeq", MySqlDbType.VarChar).Value = accountSeq;
                var reader = cmd.ExecuteReader();
                int curTrophy = 0;
                if (reader.Read())
                {
                    curTrophy = (int)reader["trophyCnt"];
                }
                reader.Close();

                if (curTrophy >= trophy)
                {
                    query = "UPDATE userlist SET trophyCnt = trophyCnt - @trophyCnt WHERE accountSeq = @accountSeq";

                    cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@trophyCnt", MySqlDbType.Int64).Value = trophy;
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = accountSeq;
                    cmd.ExecuteNonQuery();
                    return   - trophy;
                }
                else
                    return -1;
            }
        }
    }
}
