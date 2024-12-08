using MySql.Data.MySqlClient;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROJ_DCCC.HTTP.DTO.Response.RP_GetCarList;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_GetCharacterList : HTTPResponce
    {
        public class Character
        {
            public int characterNo;
            public int characterType;
            public bool isSelected;
        }

        public Character[] characters;

        public override void Process(HTTPRequest request)
        {
            var req = (RQ_GetCharacterList)request;
            using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
            {
                mysql.Open();
                string query = string.Format("SELECT characterNo FROM userlist WHERE accountSeq = @accountSeq");

                var cmd = new MySqlCommand(query, mysql);
                cmd.Parameters.Add("@accountSeq", MySqlDbType.VarChar).Value = req.characterReq.accountSeq;
                var reader = cmd.ExecuteReader();
                int characterNo = 0;
                if (reader.Read())
                {
                    characterNo = (int)reader["characterNo"];
                }
                reader.Close();

                query = string.Format("SELECT * FROM characterlist WHERE accountSeq = @accountSeq");

                cmd = new MySqlCommand(query, mysql);
                cmd.Parameters.Add("@accountSeq", MySqlDbType.VarChar).Value = req.characterReq.accountSeq;
                reader = cmd.ExecuteReader();
                var characterList = new List<Character>();
                while (reader.Read())
                {
                    var character = new Character();
                    character.characterNo = (int)reader["characterNo"] + 1;
                    character.characterType = 0;
                    character.isSelected = character.characterNo == characterNo;
                    characterList.Add(character);
                }
                characters = characterList.ToArray();
            }
            success = true;
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_GetCharacterList : HTTPRequest
    {
        public UserReqInfo characterReq;
    }
}