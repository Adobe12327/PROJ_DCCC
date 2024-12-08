using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_GiftList : HTTPResponce
    {
        public class Present
        {
            public long presentSeq;
            public long accountSeq;
            public string presentType;
            public long presentQty;
            public string recvDate;
        }
        public Present[] presents;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_GiftList)request;
            if (Utils.IsVaildToken(req.tireReq.accountSeq, req.tireReq.token))
            {
                //TODO: Add Present System...
                presents = new Present[0];
                success = true;
            }
        }
    }
}


namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_GiftList : HTTPRequest
    {
        public UserReqInfo tireReq;
    }
}