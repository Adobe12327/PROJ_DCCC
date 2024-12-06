using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJ_DCCC.HTTP.DTO.Response
{
    class CheckService : HTTPResponce
    {
        public override void Process(HTTPRequest request)
        {
           this.success = true;
        }
    }
}
