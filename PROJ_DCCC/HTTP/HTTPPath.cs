using PROJ_DCCC.HTTP.DTO.Response;
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
            { "/service/inspection/check/", new DTOs(null, typeof(RP_CheckService), true) },
            { "/user/auth/login/", new DTOs(typeof(RQ_Login), typeof(RP_Login), true) },
            { "/setting/control/", new DTOs(null, typeof(RP_ServerEventControl)) },
            { "/user/info/get/", new DTOs(typeof(RQ_UserInfo), typeof(RP_UserInfo)) },
            { "/skill/get/list/", new DTOs(typeof(RQ_CarSkillList), typeof(RP_CarSkillList)) },
            { "/service/resource/messagelist/", new DTOs(null, typeof(RP_MessageList)) },
            { "/user/car/list/", new DTOs(typeof(RQ_GetCarList), typeof(RP_GetCarList)) },
            { "/user/character/list/", new DTOs(typeof(RQ_GetCharacterList), typeof(RP_GetCharacterList)) },
            { "/user/character/slot/info/", new DTOs(typeof(RQ_SlotInfo), typeof(RP_SlotInfo)) },
            { "/shop/item/list/", new DTOs(typeof(RQ_GetItemList), typeof(RP_GetItemList)) },
            { "/invitation/list/", new DTOs(typeof(RQ_InviteList), typeof(RP_InviteList)) },
            { "/ranking/current/list/", new DTOs(typeof(RQ_GetRank), typeof(RP_GetRank)) },
        };
    }
}
