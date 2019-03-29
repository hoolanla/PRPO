using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Log
    {

        DAL.Log _DAL = new DAL.Log(); 

        public void WriteLog(Model.Log Log)
        {

            _DAL.WriteLog(Log);

        }

    }
}
