using MySql.Data.MySqlClient;
using PROJ_DCCC.DataBase;
using PROJ_DCCC.DataBase.EquipItemDB;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_BuyEquipSlot : HTTPResponce
    {
        public int remainTrophyCnt;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_BuyEquipSlot)request;
            if (Utils.IsVaildToken(req.characterReq.accountSeq, req.characterReq.token))
            {
                var remainTrophy = Utils.PayWithTrophy(req.characterReq.accountSeq, EquipItemDataBase.Instance.EquipItemCost.ExtendSlot);
                if (remainTrophy != -1)
                {
                    using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                    {
                        mysql.Open();

                        string query = "UPDATE characterslot SET slotOpen = 1 WHERE accountSeq = @accountSeq AND driverNo = @driverNo AND slotNo = @slotNo";
                        var cmd = new MySqlCommand(query, mysql);
                        cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.characterReq.accountSeq;
                        cmd.Parameters.Add("@driverNo", MySqlDbType.Int32).Value = req.characterReq.driverNo - 1;
                        cmd.Parameters.Add("@slotNo", MySqlDbType.Int32 ).Value = req.characterReq.slotNo;
                        cmd.ExecuteNonQuery();
                    }
                    remainTrophyCnt = remainTrophy;
                    success = true;
                }
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_BuyEquipSlot : HTTPRequest
    {
        public class EquipSlotInfo : UserReqInfo
        {
            public int driverNo;
            public int slotNo;
        }
        public EquipSlotInfo characterReq;
    }
}