using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class RP_ServerEventControl : HTTPResponce
    {
        public class Feature
        {
            public string featureName;
            public bool isEnable;
        }

        public bool isRewardStart;
        public bool AddCarViewFlag;
        public bool isPointUseStart;
        public int chanceFirstTrophy;
        public int chanceRetryTrophy;
        public Feature[] feature;

        public override void Process(HTTPRequest request)
        {
            feature = new Feature[Configuration.featureList.Length];
            for (int i = 0; i < Configuration.featureList.Length; i++)
            {
                feature[i] = new Feature();
                feature[i].featureName = Configuration.featureList[i].Item1;
                feature[i].isEnable = Configuration.featureList[i].Item2;
            }
            //TODO
            isRewardStart = false;
            AddCarViewFlag = false;
            isPointUseStart = false;
            chanceFirstTrophy = 1;
            chanceRetryTrophy = 1;

            success = true;
        }
    }
}
