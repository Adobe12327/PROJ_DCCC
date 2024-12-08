using MySql.Data.MySqlClient;
using PROJ_DCCC.HTTP.DTO.Request;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_EquipItemListInfo : HTTPResponce
    {
        public class EquipItemInfo
        {
            public string equipItemNokey;
            public int equipItemNo;
            public int optionNo1;
            public int optionNo2;
            public int reinforceLevel;
            public string equipItemName;
            public int saleGoldAmt;
            public string grade;
        }

        public EquipItemInfo[] equips;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_EquipItemListInfo)request;
            if (Utils.IsVaildToken(req.characterReq.accountSeq, req.characterReq.token))
            {
                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string query = "SELECT * FROM equipitemlist WHERE accountSeq = @accountSeq";

                    var cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.characterReq.accountSeq;
                    var reader = cmd.ExecuteReader();
                    var itemList = new List<EquipItemInfo>();
                    while (reader.Read())
                    {
                        var item = new EquipItemInfo();
                        item.equipItemNokey = reader["equipItemNokey"].ToString();
                        item.equipItemNo = (int)reader["equipItemNo"];
                        item.optionNo1 = (int)reader["optionNo1"];
                        item.optionNo2 = (int)reader["optionNo2"];
                        item.reinforceLevel = (int)reader["reinforceLevel"];
                        item.equipItemName = (string)reader["equipItemName"];
                        item.saleGoldAmt = (int)reader["saleGoldAmt"];
                        item.grade = (string)reader["grade"];
                        itemList.Add(item);
                    }
                    equips = itemList.ToArray();
                }
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_EquipItemListInfo : HTTPRequest
    {
        public UserReqInfo characterReq;
    }
}