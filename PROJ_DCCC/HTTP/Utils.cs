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
                string selectQuery = string.Format("SELECT accessToken FROM userlist WHERE accountSeq = @accountSeq");

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
    }
}
