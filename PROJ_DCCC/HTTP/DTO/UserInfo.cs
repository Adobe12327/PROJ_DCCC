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
    class RP_UserInfo : HTTPResponce
    {
        public class UserInfo
        {
            public int tireCnt;
            public int tireRemainSecs;
            public bool canPresent;
            public long gold;
            public int trophyCnt;
            public int characterNo;
            public int randomCharacterNo;
            public int carNo;
            public long carSeq;
            public string carClass;
            public int carAccel;
            public int carSpeed;
            public int carFuleCost;
            public long maxDistance;
            public long maxPoint;
            public long maxCombo;
            public long maxGold;
            public long maxScore;
            public long maxSpeed;
            public long goldAmt;
            public long goldUse;
            public long playCount;
            public long crashCount;
            public long jumpCount;
            public long item1Use;
            public long item2Use;
            public long item3Use;
            public long item4Use;
            public long item5Use;
            public long item6Use;
            public long item7Use;
            public long totalDistance;
            public long totalCombo;
            public long maxSpeedCrashCount;
            public long totalCarDestroyCount;
            public long friendInviteCnt;
            public long dormancyTireSendCnt;
            public long dormancyScore;
            public bool isDormancyRewardPopup;
            public bool isRewardFlag;
            public string newWeekStart;
            public string nickName;
            public string pictureUrl;
            public bool isRewardCompleteFlag;
            public bool isExistGroup;
            public int rivalWinCount;
            public bool isProfileDisplayAgreement;
            public string totalRankRewardInit;
            public int[] missions;
        }
        public UserInfo info;

        public override void Process(HTTPRequest request)
        {
            var req = (RQ_UserInfo)request;

            if (Utils.IsVaildToken(req.infoReq.accountSeq, req.infoReq.token))
            {
                success = true;
                info = new UserInfo();

                //TODO
                info.totalRankRewardInit = "202412051200";
                info.isRewardFlag = false;
                info.isRewardCompleteFlag = false;
                info.isExistGroup = false;

                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string query = string.Format("SELECT * FROM userlist WHERE accountSeq = @accountSeq");

                    var cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.infoReq.accountSeq;
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        info.nickName = (string)reader["nickName"];
                        info.pictureUrl = (string)reader["pictureUrl"];
                        info.gold = (long)reader["gold"];
                        info.trophyCnt = (int)reader["trophyCnt"];
                        info.characterNo = (int)reader["characterNo"] + 1;
                        info.randomCharacterNo = (int)reader["randomCharacterNo"] + 1;
                        info.carSeq = (long)reader["carSeq"];
                        info.isProfileDisplayAgreement = (bool)reader["isProfileDisplayAgreement"];
                        info.newWeekStart = (string)reader["newWeekStart"];
                    }
                    reader.Close();

                    query = string.Format("SELECT * FROM usertireinfo WHERE accountSeq = @accountSeq");

                    cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.infoReq.accountSeq;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        info.tireCnt = (int)reader["tireCnt"];
                        info.tireRemainSecs = (int)reader["tireRemainSecs"];
                        info.canPresent = (bool)reader["canPresent"];
                        info.dormancyTireSendCnt = (int)reader["dormancyTireSendCnt"];
                        info.dormancyScore = (int)reader["dormancyScore"];
                        info.isDormancyRewardPopup = (bool)reader["isDormancyRewardPopup"];
                    }
                    reader.Close();

                    query = string.Format("SELECT * FROM userrecords WHERE accountSeq = @accountSeq");

                    cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.infoReq.accountSeq;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        info.maxDistance = (long)reader["maxDistance"];
                        info.maxPoint = (long)reader["maxPoint"];
                        info.maxCombo = (long)reader["maxCombo"];
                        info.maxGold = (long)reader["maxGold"];
                        info.maxScore = (long)reader["maxScore"];
                        info.maxSpeed = (long)reader["maxSpeed"];
                        info.goldAmt = (long)reader["goldAmt"];
                        info.goldUse = (long)reader["goldUse"];
                        info.playCount = (long)reader["playCount"];
                        info.crashCount = (long)reader["crashCount"];
                        info.jumpCount = (long)reader["jumpCount"];
                        info.item1Use = (long)reader["item1Use"];
                        info.item2Use = (long)reader["item2Use"];
                        info.item3Use = (long)reader["item3Use"];
                        info.item4Use = (long)reader["item4Use"];
                        info.item5Use = (long)reader["item5Use"];
                        info.item6Use = (long)reader["item6Use"];
                        info.item7Use = (long)reader["item7Use"];
                        info.totalDistance = (long)reader["totalDistance"];
                        info.totalCombo = (long)reader["totalCombo"];
                        info.maxSpeedCrashCount = (long)reader["maxSpeedCrashCount"];
                        info.totalCarDestroyCount = (long)reader["totalCarDestroyCount"];
                        info.friendInviteCnt = (long)reader["friendInviteCnt"];
                    }
                    reader.Close();

                    query = string.Format("SELECT * FROM carlist WHERE carSeq = @carSeq");

                    cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@carSeq", MySqlDbType.Int64).Value = info.carSeq;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        info.carNo = (int)reader["carNo"];
                        info.carClass = (string)reader["carClass"];
                        info.carAccel = (int)reader["carAccel"];
                        info.carSpeed = (int)reader["carSpeed"];
                        info.carFuleCost = (int)reader["carFuleCost"];
                    }
                    reader.Close();

                    query = string.Format("SELECT * FROM usermissionlist WHERE accountSeq = @accountSeq");

                    cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.infoReq.accountSeq;
                    reader = cmd.ExecuteReader();
                    List<int> missionList = new List<int>();
                    while (reader.Read())
                    {
                        missionList.Add((int)reader["missionId"]);
                    }
                    reader.Close();
                    info.missions = missionList.ToArray();
                }
            }
            else
            {
                success = false;
                //TODO: ERROR CODE
            }
        }
    }
}


namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_UserInfo : HTTPRequest
    {
        public UserReqInfo infoReq;
    }
}