using GeoMed.Base;
using GeoMed.MobileService.Dto;
using GeoMed.MobileService.IData;
using GeoMed.Model.Main;
using GeoMed.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Data
{
    public class MobileRepository : BaseRepository, IMobileRepository
    {
        private readonly GMContext context;

        public MobileRepository(GMContext context) : base(context)
        {
            this.context = context;
        }

        public OperationResult<string> TestMessage()
        {
           return new OperationResult<string>() { Result ="Test message" };
        }

        public async Task<OperationResult<LoginDto>> Login(LoginDto login)
        {
            if (!context.Patients.Where(x=>!x.DeleteDate.HasValue).Any())
            {
                context.Patients.Add(new Patient() {
                
                    Username ="geomed",
                    Password="123456",
                    FirstName = "حذيفة",
                    LastName = "أصيل",
                    Birthdate = new DateTime(1998,3,21),
                    Area  = new Model.Setting.Area() { Name="سوريا-حلب-المارتيني" },
                    Career = new Model.Setting.Career() { Name = "مطور برمجيات" },
                    Reviews = new List<Review>() { new Review() { 
                    
                        Date = DateTime.Now,
                        NextReviewDate =  DateTime.Now.AddDays(12),
                        Note = "مراجعة تمت بوقت متاخر",
                        HealthCenter = new HealthCenter(){
                            Name = "المركز الصحي الأول",
                            Area = new Model.Setting.Area() {
                                Name = "سوريا - حلب - ساحة الجامعة",
                                },
                            Type =SharedKernal.Enums.HealthCenters.Center,
                            OpeningTime = TimeSpan.FromDays(0.2),
                            ClosingTime = TimeSpan.FromDays(0.8),
                            },
                        DoctorReviews =  new List<DoctorReview>(){ 
                            new DoctorReview() {
                            Description ="ألم بالمعدة بعد العشاء خفيف مكون من فاكهة وسكريات",
                            Doctor = new Doctor(){ 
                                Name = "محمد الاحمد",
                                Career = new Model.Setting.Career(){ Name ="هضمية" },
                            },
                            Recipe = "الابتعاد عن السكريات",
                            },
                            
                        }
                    }}
                });
                await context.SaveChangesAsync();
            }
            var  one =await context.Patients.Where(x => !x.DeleteDate.HasValue).Where(p=>p.Username== login.Username&& p.Password== login.Password).FirstOrDefaultAsync();
            login.Id = one?.Id ?? 0;
            return new OperationResult<LoginDto>() { OperationResultType=OperationResultTypes.Success,
                Result = login
            };
        }

        public async Task<OperationResult<HomeDto>> Home(int id)
        {

           var data =await  context.Patients.Where(x => !x.DeleteDate.HasValue).Where(p => p.Id == id).
                Select(p => new HomeDto() {
                        FullName = p.FirstName +" "+p.LastName,
                        Area=p.Area.Name,
                        Career = p.Career.Name,
                        Reviews = p.Reviews.Where(r=>!r.DeleteDate.HasValue).
                        Select(r=> new ReviewDto() { 
                        Date =r.Date,
                        Note =r.Note,
                        NextReviewDate =r.NextReviewDate,
                        Description= r.DoctorReviews.FirstOrDefault().Description,
                        DoctorCareer= r.DoctorReviews.FirstOrDefault().Doctor.Career.Name,
                        Recipe = r.DoctorReviews.FirstOrDefault().Recipe,
                        HealthCenterName = r.HealthCenter.Name,
                    })
                }).FirstOrDefaultAsync();

            return new OperationResult<HomeDto>()
            {
                OperationResultType = OperationResultTypes.Success,
                Result = data,
            };
        }

        public async Task<OperationResult<bool>> Register(RegisterDto register)
        {
            if(string.IsNullOrEmpty(register.Username.Trim()) || 
                string.IsNullOrEmpty(register.Password.Trim()) ||
                string.IsNullOrEmpty(register.FullName.Trim()) ||
                string.IsNullOrEmpty(register.Area.Trim()) ||
                string.IsNullOrEmpty(register.Career.Trim())
                )
            {
                return new OperationResult<bool>()
                {
                    OperationResultType = OperationResultTypes.Failed,
                    Result = false
                };
            }
            var one = await context.Patients.Where(x => !x.DeleteDate.HasValue)
                .Where(p => p.Username == register.Username).FirstOrDefaultAsync();
            if(one != null)
            {
                return new OperationResult<bool>()
                {
                    OperationResultType = OperationResultTypes.Failed,
                    Result = false
                };
            }

            context.Patients.Add( new Patient() {
                 Username= register.Username,
                 Password = register.Password,
                 FirstName = register.FullName,
                Area = new Model.Setting.Area() { Name = register.Area },
                Career = new Model.Setting.Career() { Name = register.Career },
            } );

           await context.SaveChangesAsync();

            return new OperationResult<bool>()
            {
                OperationResultType = OperationResultTypes.Failed,
                Result = true
            };
        }
    }
}
