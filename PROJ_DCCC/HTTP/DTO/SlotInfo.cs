using MySql.Data.MySqlClient;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROJ_DCCC.HTTP.DTO.Response.RP_GetCharacterList;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_SlotInfo : HTTPResponce
    {
        public class Slot
        {
            public int driverNo;
            public int slotNo;
            public string equipItemNoKey;
            public int equipItemNo;
            public int optionNo1;
            public int optionNo2;
            public int reinforceLevel;
            public string equipItemName;
            public int saleGoldAmt;
            public string grade;
            public bool slotOpen;
        }
        public Slot[] slots;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_SlotInfo)request;
            if (Utils.IsVaildToken(req.characterReq.accountSeq, req.characterReq.token))
            {
                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string query = string.Format("SELECT * FROM characterslot WHERE accountSeq = @accountSeq AND driverNo = @driverNo");

                    var cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.characterReq.accountSeq;
                    cmd.Parameters.Add("@driverNo", MySqlDbType.Int32).Value = req.characterReq.driverNo - 1;
                    var reader = cmd.ExecuteReader();
                    var slotList = new List<Slot>();
                    while (reader.Read())
                    {
                        var slot = new Slot();
                        slot.driverNo = (int)reader["driverNo"];
                        slot.slotNo = (int)reader["slotNo"];
                        slot.equipItemNoKey = reader["equipItemNoKey"].ToString();
                        slotList.Add(slot);
                    }
                    slots = slotList.ToArray();
                    reader.Close();

                    foreach(var slot in slots)
                    {
                        query = string.Format("SELECT * FROM equipitemlist WHERE accountSeq = @accountSeq AND equipItemNoKey = @equipItemNoKey");

                        cmd = new MySqlCommand(query, mysql);
                        cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.characterReq.accountSeq;
                        cmd.Parameters.Add("@equipItemNoKey", MySqlDbType.Int64).Value = slot.equipItemNoKey;
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            slot.equipItemNo = (int)reader["equipItemNo"];
                            slot.optionNo1 = (int)reader["optionNo1"];
                            slot.optionNo2 = (int)reader["optionNo2"];
                            slot.reinforceLevel = (int)reader["reinforceLevel"];
                            slot.equipItemName = (string)reader["equipItemName"];
                            slot.saleGoldAmt = (int)reader["saleGoldAmt"];
                            slot.grade = (string)reader["grade"];
                        }
                        reader.Close();
                    }
                }
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_SlotInfo : HTTPRequest
    {
        public class CharacterReq : UserReqInfo
        {
            public int driverNo;
        }
        public CharacterReq characterReq;
    }
}