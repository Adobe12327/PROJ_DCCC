﻿using PROJ_DCCC.HTTP.DTO.Response;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP
{
    struct DTOs
    {
        public Type request;
        public Type responce;
        public bool noEncryption = false;
        public DTOs(Type request, Type responce)
        {
            this.request = request;
            this.responce = responce;
        }

        public DTOs(Type request, Type responce, bool noEncryption)
        {
            this.request = request;
            this.responce = responce;
            this.noEncryption = noEncryption;
        }
    }
    class HTTPPath
    {
        public static Dictionary<string, DTOs> PathList = new Dictionary<string, DTOs>() {
            { "/service/inspection/check/", new DTOs(null, typeof(RP_CheckService), true)},
            { "/user/auth/login/", new DTOs(typeof(RQ_Login), typeof(RP_Login), true) }
        };
    }
}