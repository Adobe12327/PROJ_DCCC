using MySql.Data.MySqlClient;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_TireCheck : HTTPResponce
    {
        public class TireInfo
        {
            public int tireCnt;
            public int remainTime;
        }

        public TireInfo result;

        public override void Process(HTTPRequest request)
        {
            var req = (RQ_TireCheck)request;
            if (Utils.IsVaildToken(req.tireCheckReq.accountSeq, req.tireCheckReq.token))
            {
                result = new TireInfo();
                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string selectQuery = "SELECT tireCnt, tireRemainSecs FROM usertireinfo WHERE accountSeq = @accountSeq";

                    var cmd = new MySqlCommand(selectQuery, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.tireCheckReq.accountSeq;
                    var reader = cmd.ExecuteReader();
                    var indices = new List<int>();
                    if (reader.Read())
                    {
                        result.tireCnt = (int)reader["tireCnt"];
                        result.remainTime = (int)reader["tireRemainSecs"];
                    }
                }
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_TireCheck : HTTPRequest
    {
        public UserReqInfo tireCheckReq;
    }
}