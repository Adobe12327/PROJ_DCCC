using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.DataBase.CarDB
{
    public class TurningStatusDB
    {
        public float MaxSpeed;
        public float SpeedPerSecond;
        public float NextStepSpeed;
        public float NextSpeedPerSecond;
        public float OilMileage;
    }
    public class UpgradeCostDB
    {
        public int R;
        public int S;
        public int A;
        public int B;
    }
    public class TurningCostDB
    {
        public class CarClassData
        {
            public class TurningTypeData
            {
                public int[] CostArray;
                public string TurningType;
            }
            public string CarClassType;
            public TurningTypeData[] TurningTypeDataArray;
        }
        public string CarIconAtlas;
        public int OriginalSkill;
        public CarClassData[] CarClassDataArray;
    }
    public class InfoDB
    {
        public class CarData
        {
            public class CarClassData
            {
                public string CarClassType;
                public int MaxSpeed;
                public int CarWeight;
                public int SpeedPerSecond;
                public int NextStepSpeed;
                public float NextSpeedPerSecond;
                public float OilMileage;
            }
            public string CarName;
            public int CarIndex;
            public string StartCarClassType;
            public int CostGold;
            public int UnlockTrophy;
            public bool Preminum;
            public bool NewCar;
            public bool EventCar;
            public bool RivalCar;
            public bool IsRobot;
            public bool HasMission;
            public bool IsViewStore;
            public bool IsPossibleTradeCar;
            public string MissionType;
            public bool IsGotyaEvent;
            public int GotyaCost;
            public int GotyaRetryCost;
            public string CarIconAtlas;
            public int OriginalSkill;
            public int SynergyDriver;
            public bool IsEffectByClass;
            public bool IsAnotherNitro;
            public int BossConnectIndex;
            public CarClassData[] CarClassDataArray;
        }
        public CarData[] CarDataArray;
    }
    public class CarDataBase
    {
        public TurningStatusDB TurningStatusDB;
        public UpgradeCostDB UpgradeCostDB;
        public TurningCostDB TurningCostDB;
        public InfoDB CarInfoDB;

        public static CarDataBase Instance;

        public InfoDB.CarData GetCarDataWithID(int CarIndex)
        {
            foreach(var c in CarInfoDB.CarDataArray)
            {
                if (c.CarIndex == CarIndex)
                    return c;
            }
            return null;
        }
    }
}
