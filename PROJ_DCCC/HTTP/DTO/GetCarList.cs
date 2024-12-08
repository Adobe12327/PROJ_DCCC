using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Ocsp;
using PROJ_DCCC.HTTP.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROJ_DCCC.HTTP.DTO.Response.RP_MessageList;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_GetCarList : HTTPResponce
    {
        public class Car
        {
            public long carSeq;
            public int carNo;
            public string carClass;
            public int carAccel;
            public int carSpeed;
            public int carFuleCost;
            public bool isSelected;
        }

        public Car[] cars;

        public override void Process(HTTPRequest request)
        {
            var req = (RQ_GetCarList)request;
            using (MySqlConnection mysql = new MySqlConnection(Configuration.connStr))
            {
                mysql.Open();
                string query = string.Format("SELECT carSeq FROM userlist WHERE accountSeq = @accountSeq");

                var cmd = new MySqlCommand(query, mysql);
                cmd.Parameters.Add("@accountSeq", MySqlDbType.VarChar).Value = req.carReq.accountSeq;
                var reader = cmd.ExecuteReader();
                long carSeq = 0;
                if (reader.Read())
                {
                    carSeq = (long)reader["carSeq"];
                }
                reader.Close();

                query = string.Format("SELECT * FROM carlist WHERE accountSeq = @accountSeq");

                cmd = new MySqlCommand(query, mysql);
                cmd.Parameters.Add("@accountSeq", MySqlDbType.VarChar).Value = req.carReq.accountSeq;
                reader = cmd.ExecuteReader();
                var carList = new List<Car>();
                while (reader.Read())
                {
                    var car = new Car();
                    car.carSeq = (long)reader["carSeq"];
                    car.carNo = (int)reader["carNo"];
                    car.carClass = (string)reader["carClass"];
                    car.carAccel = (int)reader["carAccel"];
                    car.carSpeed = (int)reader["carSpeed"];
                    car.carFuleCost = (int)reader["carFuleCost"];
                    car.isSelected = car.carSeq == carSeq;
                    carList.Add(car);
                }
                cars = carList.ToArray();
            }
            success = true;
        }
    }
}

namespace PROJ_DCCC.HTTP.DTO.Request
{
    class RQ_GetCarList : HTTPRequest
    {
        public UserReqInfo carReq;
    }
}