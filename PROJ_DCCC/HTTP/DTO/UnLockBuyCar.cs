using MySql.Data.MySqlClient;
using PROJ_DCCC.DataBase;
using PROJ_DCCC.DataBase.CarDB;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_UnLockBuyCar : HTTPResponce
    {
        public int remainTrophyCnt;
        public long carSeq;
        public int skillNo;
        public int curPoint;
        public int bossType;
        public int[] missions;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_UnLockBuyCar)request;



            if (Utils.IsVaildToken(req.shopReq.accountSeq, req.shopReq.token))
            {
                var car = CarDataBase.Instance.GetCarDataWithID(req.shopReq.carNo);
                var remainTrophy = Utils.PayWithTrophy(req.shopReq.accountSeq, car.UnlockTrophy);
                if (remainTrophy != -1)
                {
                    using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                    {
                        mysql.Open();
                        string query = "INSERT INTO carlist(accountSeq, carNo, carClass, carAccel, carSpeed, carFuleCost) VALUES(@accountSeq, @carNo, @carClass, 1, 1, 1)";
                        
                        var cmd = new MySqlCommand(query, mysql);
                        cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.shopReq.accountSeq;
                        cmd.Parameters.Add("@carNo", MySqlDbType.Int32).Value = req.shopReq.carNo;
                        cmd.Parameters.Add("@carClass", MySqlDbType.VarChar).Value = car.StartCarClassType;
                        cmd.ExecuteNonQuery();

                        var boughtCarSeq = cmd.LastInsertedId;

                        query = "INSERT INTO carskilllist VALUES(@accountSeq, @ogSkill, @carNo, 1, 'Y', 1)";
                        cmd = new MySqlCommand(query, mysql);
                        cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.shopReq.accountSeq;
                        cmd.Parameters.Add("@ogSkill", MySqlDbType.Int32).Value = car.OriginalSkill;
                        cmd.Parameters.Add("@carNo", MySqlDbType.Int32).Value = req.shopReq.carNo;
                        cmd.ExecuteNonQuery();

                        remainTrophyCnt = remainTrophy;
                        carSeq = boughtCarSeq;
                        skillNo = car.OriginalSkill;

                        //TODO
                        curPoint = 0;
                        bossType = 0;

                        //TODO: Mission
                        missions = new int[0];
                    }

                    success = true;
                }
                else
                {
                    success = false;
                    //TODO: ERROR CODE
                }
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_UnLockBuyCar : HTTPRequest
    {
        public class ShopReq : UserReqInfo
        {
            public int carNo;
        }
        public ShopReq shopReq;
    }
}