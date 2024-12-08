using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.DataBase.EquipItemDB
{
    public class EquipItemCost
    {
        public int GotchaGold;
        public int GotchaTrophy;
        public int ExtendInven;
        public int ExtendSlot;
    }
    public class EquipItemDataBase
    {
        public static EquipItemDataBase Instance;
        public EquipItemCost EquipItemCost;
    }
}
