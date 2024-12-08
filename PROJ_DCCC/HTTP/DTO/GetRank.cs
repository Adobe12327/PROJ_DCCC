using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_GetRank : HTTPResponce
    {
        public class FriendInfo
        {
            public string userId;
            public string gameMode;
            public long accountSeq;
            public long score;
            public int carNo;
            public int carClass;
            public bool canPresent;
            public bool sentPresent;
            public bool boastReject;
            public int carX;
            public int carY;
            public bool matchRejectFlag;
            public int grade;
            public bool isDormancy;
            public int ladderClassNo;
        }
        public FriendInfo[] friends;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_GetRank)request;
            if (Utils.IsVaildToken(req.rankReq.accountSeq, req.rankReq.token))
            {
                //TODO: Add Ranking System..........
                friends = new FriendInfo[0];
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_GetRank : HTTPRequest
    {
        public class RankReq : UserReqInfo
        {
            public string[] friendsUserIds;
            public string channelCd;
        }

        public RankReq rankReq;
    }
}