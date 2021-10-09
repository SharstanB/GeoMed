using GeoMed.Base;
using GeoMed.MobileService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.IData
{
    public interface IMobileRepository
    {
        Task<OperationResult<HomeDto>> Home(int id);
        Task<OperationResult<LoginDto>> Login(LoginDto login);
        Task<OperationResult<bool>> Register(RegisterDto register);
        OperationResult<string> TestMessage();
    }
}
