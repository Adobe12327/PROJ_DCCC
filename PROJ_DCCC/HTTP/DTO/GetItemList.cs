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
    class RP_GetItemList : HTTPResponce
    {
        public class ShopItem
        {
            public int itemCode;
            public int itemCount;
        }

        public ShopItem[] items;
        public int toolboxRetryCount;
        public int toolboxRebuyGoldAmt;

        public override void Process(HTTPRequest request)
        {
            var req = (RQ_GetItemList)request;
            if (Utils.IsVaildToken(req.shopReq.accountSeq, req.shopReq.token))
            {
                //TODO
                toolboxRetryCount = 0;
                toolboxRebuyGoldAmt = 0;

                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string selectQuery = string.Format("SELECT * FROM useritemlist WHERE accountSeq = @accountSeq");

                    var cmd = new MySqlCommand(selectQuery, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.shopReq.accountSeq;
                    var reader = cmd.ExecuteReader();
                    var itemList = new List<ShopItem>();
                    while (reader.Read())
                    {
                        var item = new ShopItem();
                        item.itemCode = (int)reader["itemCode"];
                        item.itemCount = (int)reader["itemCount"];
                        itemList.Add(item);
                    }
                    items = itemList.ToArray();
                }
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_GetItemList : HTTPRequest
    {
        public UserReqInfo shopReq;
    }
}