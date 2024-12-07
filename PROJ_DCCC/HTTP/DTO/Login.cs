using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_Login : HTTPResponce
    {
        public override void Process(HTTPRequest request)
        {
            RQ_Login req = (RQ_Login)request;
            Console.WriteLine(req.loginReq.register);
            this.success = true;
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class LoginReq
    {
        public string userId;
        public string accessToken;
        public string channelId;
        public string channelCd;
        public bool register;
    }
    class RQ_Login : HTTPRequest
    {
        public LoginReq loginReq;
    }
}