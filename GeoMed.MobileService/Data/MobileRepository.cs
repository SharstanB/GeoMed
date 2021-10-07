using GeoMed.Base;
using GeoMed.MobileService.IData;
using GeoMed.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Data
{
    public class MobileRepository : BaseRepository, IMobileRepository
    {
        public MobileRepository(GMContext context) : base(context)
        {

        }

        public OperationResult<string> TestMessage()
        {
           return new OperationResult<string>() { Result ="Test message" };
        }
        
    }
}
