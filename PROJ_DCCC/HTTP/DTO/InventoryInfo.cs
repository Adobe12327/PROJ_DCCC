using MySql.Data.MySqlClient;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_InventoryInfo : HTTPResponce
    {
        public int inventorySpaceCnt;
        public int equipItemCnt;

        public override void Process(HTTPRequest request)
        {
            var req = (RQ_InventoryInfo)request;
            if (Utils.IsVaildToken(req.characterReq.accountSeq, req.characterReq.token))
            {
                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string selectQuery = string.Format("SELECT inventorySpaceCnt, equipItemCnt FROM userlist WHERE accountSeq = @accountSeq");

                    var cmd = new MySqlCommand(selectQuery, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.characterReq.accountSeq;
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        inventorySpaceCnt = (int)reader["inventorySpaceCnt"];
                        equipItemCnt = (int)reader["equipItemCnt"];
                    }
                }
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_InventoryInfo : HTTPRequest
    {
        public UserReqInfo characterReq;
    }
}