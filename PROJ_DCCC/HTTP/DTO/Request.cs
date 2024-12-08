using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO
{
    class HTTPRequest
    {

    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    public class UserReqInfo
    {
        public long token;
        public long accountSeq;
    }
}