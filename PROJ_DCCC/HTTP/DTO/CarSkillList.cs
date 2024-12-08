using MySql.Data.MySqlClient;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_CarSkillList : HTTPResponce
    {
        public class Skill
        {
            public int skillNo;
            public int carNo;
            public int skillLevel;
            public string equipFlag;
            public int skillType;
        }
        public Skill[] skillList;
        public override void Process(HTTPRequest request)
        {
            var req = (RQ_CarSkillList)request;
            if (Utils.IsVaildToken(req.skillReq.accountSeq, req.skillReq.token))
            {
                using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
                {
                    mysql.Open();
                    string query = "SELECT * FROM carskilllist WHERE accountSeq = @accountSeq";

                    var cmd = new MySqlCommand(query, mysql);
                    cmd.Parameters.Add("@accountSeq", MySqlDbType.Int64).Value = req.skillReq.accountSeq;
                    var reader = cmd.ExecuteReader();
                    var skills = new List<Skill>();
                    while (reader.Read())
                    {
                        var skill = new Skill();
                        skill.skillNo = (int)reader["skillNo"];
                        skill.carNo = (int)reader["carNo"];
                        skill.skillLevel = (int)reader["skillLevel"];
                        skill.equipFlag = (string)reader["equipFlag"];
                        skill.skillType = (int)reader["skillType"];
                        skills.Add(skill);
                    }
                    skillList = skills.ToArray();
                }
                success = true;
            }
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_CarSkillList : HTTPRequest
    {
        public UserReqInfo skillReq;
    }
}