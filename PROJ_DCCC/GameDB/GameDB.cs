using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PROJ_DCCC.DataBase.CarDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.DataBase
{
    class GameDB
    {
        public static CarDataBase CarDB;
        public GameDB()
        {
            CarDB = JsonConvert.DeserializeObject<CarDataBase>(JObject.Parse(File.ReadAllText(@"DataBase\CarDataBase.json")).SelectToken("CarDataBase").ToString());
        }
    }
}
