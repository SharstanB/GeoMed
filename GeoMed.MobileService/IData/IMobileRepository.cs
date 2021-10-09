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
        Task<OperationResult<bool>> AddReview(int id, ReviewDto review);
        Task<OperationResult<DoctorsDto>> Doctors(int id);
        Task<OperationResult<HomeDto>> Home(int id);
        Task<OperationResult<LoginDto>> Login(LoginDto login);
        Task<OperationResult<ProfileDto>> Profile(int id);
        Task<OperationResult<bool>> Register(RegisterDto register);
        Task<OperationResult<InfoReviewsDto>> Reviews(int id);
        OperationResult<string> TestMessage();
    }
}
