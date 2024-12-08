using MySql.Data.MySqlClient;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_SelectCharacter : HTTPResponce
    {

        public override void Process(HTTPRequest request)
        {
            var req = (RQ_SelectCharacter)request;
            if (Utils.IsVaildToken(req.characterReq.accountSeq, req.characterReq.token))
            {
                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();

                    string query = "UPDATE userlist SET characterNo = @characterNo WHERE accountSeq = @accountSeq";
                    var cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@characterNo", MySqlDbType.Int32).Value = req.characterReq.characterNo - 1;
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.characterReq.accountSeq;
                    cmd.ExecuteNonQuery();
                }

                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_SelectCharacter : HTTPRequest
    {
        public class SelectInfo : UserReqInfo
        {
            public int characterNo;
        }
        public SelectInfo characterReq;
    }
}