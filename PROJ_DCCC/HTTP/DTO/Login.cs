using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class LoginRes
    {
        public long accountSeq;
        public bool registered;
        public bool purchased;
        public bool newPresent;
        public string newWeekStart;
        public bool newWeek;
        public bool takeTrophy;
        public bool awardYN;
        public int attend_ivalue;
        public int attend_resultCode;
    }
    class RP_Login : HTTPResponce
    {
        public long securityToken;
        public string cryptoKey;
        public string initialVector;
        public string blockExpire;
        public LoginRes result;

        public override void Process(HTTPRequest request)
        {
            RQ_Login req = (RQ_Login)request;
            Console.WriteLine(req.loginReq.register);

            cryptoKey = Convert.ToBase64String(Configuration.aesKey);
            initialVector = Convert.ToBase64String(Configuration.aesIV);
            result = new LoginRes();

            using(MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
            {
                mysql.Open();       
                string selectQuery = string.Format("SELECT accountSeq, accessToken FROM userlist WHERE userId = @userId");

                var cmd = new MySqlCommand(selectQuery, mysql);
                cmd.Parameters.Add("@userId", MySqlDbType.VarChar).Value = req.loginReq.userId;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result.accountSeq = (long)reader["accountSeq"];
                    securityToken = (long)reader["accessToken"];
                    success = true;
                }
                else
                {
                    //TODO: Auto Register
                    throw new NotImplementedException("오토레지");
                }
            }


            //TODO
            result.newPresent = false;
            result.awardYN = false;
            result.purchased = false;
            result.newWeekStart = "202412081200";
            result.newWeek = false;
            result.takeTrophy = false;
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