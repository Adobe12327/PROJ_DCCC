using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_GlobalRankingInfo : HTTPResponce
    {
        public class LadderClass
        {
            public int ladderClassNo;
            public string standType;
            public float startValue;
            public float endValue;
            public int upgradeRange;
            public int downGradeRange;
            public string rewardTypeName;
            public int rewardQuantity;
        }
        public LadderClass[] classList;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_GlobalRankingInfo)request;
            if (Utils.IsVaildToken(req.ladderReq.accountSeq, req.ladderReq.token))
            {
                classList = new LadderClass[1] { new LadderClass() };
                classList[0].ladderClassNo = 14;
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_GlobalRankingInfo : HTTPRequest
    {
        public UserReqInfo ladderReq;
    }
}