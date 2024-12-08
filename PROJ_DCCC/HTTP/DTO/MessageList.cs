using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROJ_DCCC.HTTP.DTO.Response.RP_CarSkillList;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    /*
    001 - GiftTire
    002 - InviteFriend
    003 - Boast
    004 - GiftTrophy
    005 - PleaseTrophy
    006 - RequestVersus
    007 - LoadingTip
    008 - GiftTireToDormancyUser
    009 - TimeAttackBoast
    */
    class RP_MessageList : HTTPResponce
    {
        public class Message
        {
            public string code;
            public string message;
        }
        public Message[] messages;
        public override void Process(HTTPRequest request)
        {
            using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
            {
                mysql.Open();
                string query = string.Format("SELECT * FROM messagelist");

                var cmd = new MySqlCommand(query, mysql);
                var reader = cmd.ExecuteReader();
                var msgs = new List<Message>();
                while (reader.Read())
                {
                    var msg = new Message();
                    msg.code = (string)reader["code"];
                    msg.message = (string)reader["message"];
                    msgs.Add(msg);
                }
                messages = msgs.ToArray();
            }
            success = true;
        }
    }
}
