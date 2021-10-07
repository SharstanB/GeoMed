using GeoMed.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.IData
{
    public interface IMobileRepository
    {
        OperationResult<string> TestMessage();
    }
}
