using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_InviteList : HTTPResponce
    {
        public class Invitation
        {
            public string userId;
            public string inviteDate;
        }
        public Invitation[] invitations;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_InviteList)request;
            if (Utils.IsVaildToken(req.invitationReq.accountSeq, req.invitationReq.token))
            {
                //TODO: Implement Invitation System....
                invitations = new Invitation[0];
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_InviteList : HTTPRequest
    {
        public UserReqInfo invitationReq;
    }
}