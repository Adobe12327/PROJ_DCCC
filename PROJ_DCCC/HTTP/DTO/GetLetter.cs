using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_GetLetter : HTTPResponce
    {
        public int[] indexValues;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_GetLetter)request;
            if (Utils.IsVaildToken(req.eventReq.accountSeq, req.eventReq.token))
            {
                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string selectQuery = string.Format("SELECT indexValue FROM letterevent WHERE accountSeq = @accountSeq");

                    var cmd = new MySqlCommand(selectQuery, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.eventReq.accountSeq;
                    var reader = cmd.ExecuteReader();
                    var indices = new List<int>();
                    while (reader.Read())
                    {
                        indices.Add((int)reader["indexValue"]);
                    }
                    indexValues = indices.ToArray();
                }
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_GetLetter : HTTPRequest
    {
        public UserReqInfo eventReq;
    }
}