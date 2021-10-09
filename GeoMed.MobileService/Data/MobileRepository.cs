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
            return new OperationResult<string>() { Result = "Test message" };
        }

        public async Task<OperationResult<LoginDto>> Login(LoginDto login)
        {
            if (!context.Patients.Where(x => !x.DeleteDate.HasValue).Any())
            {
                context.Patients.Add(new Patient() {

                    Username = "geomed",
                    Password = "123456",
                    FirstName = "حذيفة",
                    LastName = "أصيل",
                    Birthdate = new DateTime(1998, 3, 21),
                    Area = new Model.Setting.Area() { Name = "سوريا-حلب-المارتيني" },
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
            var one = await context.Patients.Where(x => !x.DeleteDate.HasValue).Where(p => p.Username == login.Username && p.Password == login.Password).FirstOrDefaultAsync();
            login.Id = one?.Id ?? 0;
            return new OperationResult<LoginDto>() { OperationResultType = OperationResultTypes.Success,
                Result = login
            };
        }

        public async Task<OperationResult<HomeDto>> Home(int id)
        {

            var data = await context.Patients.Where(x => !x.DeleteDate.HasValue).Where(p => p.Id == id).
                 Select(p => new HomeDto() {
                     FullName = p.FirstName + " " + p.LastName,
                     Area = p.Area.Name,
                     Career = p.Career.Name,
                     Reviews = p.Reviews.Where(r => !r.DeleteDate.HasValue).OrderByDescending(r=>r.Date).
                         Select(r => new ReviewDto() {
                             Date = r.Date,
                             Note = r.Note,
                             NextReviewDate = r.NextReviewDate,
                             Description = r.DoctorReviews.FirstOrDefault().Description,
                             Doctor = new NominalDto() { 
                                 Id = r.DoctorReviews.FirstOrDefault().Doctor.Id,
                                  Name = r.DoctorReviews.FirstOrDefault().Doctor.Name,
                                  Career = r.DoctorReviews.FirstOrDefault().Doctor.Career.Name,
                             },
                             Recipe = r.DoctorReviews.FirstOrDefault().Recipe,
                             HealthCenter = new NominalDto()
                             {
                                 Id = r.HealthCenter.Id,
                                 Name = r.HealthCenter.Name,
                                 Area = r.HealthCenter.Area.Name
                             },
                         })
                 }).FirstOrDefaultAsync();

            data.Reviews = data.Reviews.Take(3);

            return new OperationResult<HomeDto>()
            {
                OperationResultType = OperationResultTypes.Success,
                Result = data,
            };
        }

        public async Task<OperationResult<bool>> Register(RegisterDto register)
        {
            if (string.IsNullOrEmpty(register.Username.Trim()) ||
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
            if (one != null)
            {
                return new OperationResult<bool>()
                {
                    OperationResultType = OperationResultTypes.Failed,
                    Result = false
                };
            }

            context.Patients.Add(new Patient() {
                Username = register.Username,
                Password = register.Password,
                FirstName = register.FullName,
                Area = new Model.Setting.Area() { Name = register.Area },
                Career = new Model.Setting.Career() { Name = register.Career },
            });

            await context.SaveChangesAsync();

            return new OperationResult<bool>()
            {
                OperationResultType = OperationResultTypes.Failed,
                Result = true
            };
        }

        public async Task<OperationResult<InfoReviewsDto>> Reviews(int id)
        {

            var data = await context.Patients.Where(x => !x.DeleteDate.HasValue).Where(p => p.Id == id).
                 Select(p => new InfoReviewsDto()
                 {
                     FullName = p.FirstName + " " + p.LastName,
                     Area = p.Area.Name,
                     Career = p.Career.Name,
                     Reviews = p.Reviews.Where(r => !r.DeleteDate.HasValue).OrderByDescending(r => r.Date).
                         Select(r => new ReviewDto()
                         {
                             Date = r.Date,
                             Note = r.Note,
                             NextReviewDate = r.NextReviewDate,
                             Description = r.DoctorReviews.FirstOrDefault().Description,
                             Doctor = new NominalDto()
                             {
                                 Id = r.DoctorReviews.FirstOrDefault().Doctor.Id,
                                 Name = r.DoctorReviews.FirstOrDefault().Doctor.Name,
                                 Career = r.DoctorReviews.FirstOrDefault().Doctor.Career.Name,
                             },
                             Recipe = r.DoctorReviews.FirstOrDefault().Recipe,
                             HealthCenter = new NominalDto() { 
                                 Id=r.HealthCenter.Id,
                                 Name = r.HealthCenter.Name,
                                 Area = r.HealthCenter.Area.Name,
                             },
                         }),
                 }).FirstOrDefaultAsync();

            data.Doctors = await context.Doctors.Where(x => !x.DeleteDate.HasValue).Select(x => new NominalDto()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            data.HealthCenters = await context.HealthCenters.Where(x => !x.DeleteDate.HasValue).Select(x => new NominalDto()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new OperationResult<InfoReviewsDto>()
            {
                OperationResultType = OperationResultTypes.Success,
                Result = data,
            };
        }


        public async Task<OperationResult<bool>> AddReview(int id,ReviewDto review)
        {
            if(review.HealthCenter.Id ==0 ||
                review.Doctor.Id==0 || string.IsNullOrEmpty(review.Recipe.Trim())
                )
            {
                return new OperationResult<bool>()
                {
                    OperationResultType = OperationResultTypes.Failed,
                    Result = false,
                };
            }

            var set = new Review()
            {
                Date = DateTime.Now,
                NextReviewDate = DateTime.Now.AddDays(11),
                Note = review.Note,
                PatientId = id,
                HealthCenterId =review.HealthCenter.Id,
                DoctorReviews = new List<DoctorReview>() { new DoctorReview() {
                Description = review.Description,
                DoctorId = review.Doctor.Id,
                Recipe =review.Recipe,
                } },
            };

            await context.AddAsync(set);

            await context.SaveChangesAsync();

            return new OperationResult<bool>()
            {
                OperationResultType = OperationResultTypes.Success,
                Result = true,
            };
        }


        public async Task<OperationResult<DoctorsDto>> Doctors(int id)
        {
            var _doctor = await context.Doctors.Where(x => !x.DeleteDate.HasValue).FirstOrDefaultAsync();
            var doctor = new Doctor();
            if (_doctor is null)
            {
                _doctor = new Doctor() {
                    Name = "طبيب افتراضي",
                    Career = new Model.Setting.Career() { Name="عمل افتراضي" },
                };
                 context.Add(_doctor);
                 await context.SaveChangesAsync();
            }

            if (!await context.Patients.Where(x => !x.DeleteDate.HasValue).Where(p => p.Id == id).Select(x=>x.Chats).AnyAsync())
            {
                List<Chat> chats = new List<Chat>();

                for (int i=0;i<17;i++)
                {
                    chats.Add(new Chat() {
                        Date =DateTime.Now,
                        Message =$"رسالة {i}",
                        PatientId =id,
                        DoctorId = _doctor.Id,
                        HasSeen = i%3==0,
                    });
                }
                await context.Chats.AddRangeAsync(chats);
                await context.SaveChangesAsync();
            }


            var data =  context.Patients.Include(x=>x.Chats).ThenInclude(x=>x.Doctor)
                .ThenInclude(x => x.Career).Where(x => !x.DeleteDate.HasValue).Where(p => p.Id == id).
                Select(x => new 
                {
                    FullName = x.FirstName + " " + x.LastName,
                    Area = x.Area.Name,
                    Career = x.Career.Name,
                    Chats = x.Chats.Where(x=>!x.DeleteDate.HasValue).ToList(),
                   
                }).AsEnumerable().Select(x=> new DoctorsDto() {
                
                    FullName = x.FullName,
                    Area = x.Area,
                    Career = x.Career,
                    Doctors = x.Chats.GroupBy(x => x.DoctorId).Select(c => new ChatDoctorDto()
                    {
                        Id = c.FirstOrDefault().Doctor.Id,
                        Name = c.FirstOrDefault().Doctor.Name,
                        Career = c.FirstOrDefault().Doctor.Career.Name,
                        Chats = c.OrderBy(x => x.Date).Select(h => new ChatDto() { Date = h.Date, Me = !h.HasSeen, Message = h.Message }).ToList()
                    }).ToList()
                }
                ).FirstOrDefault();

            var allDoctors = await context.Doctors.Where(x => !x.DeleteDate.HasValue).
                Where(x=> !data.Doctors.Select(x=>x.Id).Contains(x.Id)).Select(x => new ChatDoctorDto()
            {
                Id = x.Id,
                Name = x.Name,
                Chats = new List<ChatDto>(),
            }).ToListAsync();

            data.Doctors.AddRange(allDoctors);

            return new OperationResult<DoctorsDto>()
            {
                OperationResultType = OperationResultTypes.Success,
                Result = data,
            };
        }


        public async Task<OperationResult<ProfileDto>> Profile(int id)
        {

            if(!context.Kindreds.Where(x => !x.DeleteDate.HasValue).Any())
            {
                var newpa = new Patient()
                {
                    Username = "demo",
                    Password = "demo",
                    FirstName = "محمد",
                    LastName = "أصيل",
                    Birthdate = new DateTime(1998, 3, 21),
                    Area = new Model.Setting.Area() { Name = "سوريا-حلب-المشارقة" },
                    Career = new Model.Setting.Career() { Name = "صانع حلوة" },
                    Reviews = new List<Review>() { new Review() {

                        Date = DateTime.Now,
                        NextReviewDate =  DateTime.Now.AddDays(12),
                        Note = "محجوز مسبقا",
                        HealthCenter = new HealthCenter(){
                            Name = "عيادة الطبيب علي العلي",
                            Area = new Model.Setting.Area() {
                                Name = "سوريا - حلب - الرازي"
                                },
                            Type =SharedKernal.Enums.HealthCenters.Clinic,
                            OpeningTime = TimeSpan.FromDays(0.2),
                            ClosingTime = TimeSpan.FromDays(0.8),
                            },
                        DoctorReviews =  new List<DoctorReview>(){
                            new DoctorReview() {
                            Description ="أانشداد عضلي",
                            Doctor = new Doctor(){
                                Name = "علي العلي",
                                Career = new Model.Setting.Career(){ Name ="عصبية" },
                            },
                            Recipe = "مرهم",
                            },

                        }
                    }}
                };

                context.Patients.Add(newpa);
                await context.SaveChangesAsync();

                 context.Kindreds.Add(new Kindred() {
                     Level = SharedKernal.Enums.KindredLevels.Brother,
                     PatientLeftId =  id,
                     PatientRightId = newpa.Id,
                });

                context.Kindreds.Add(new Kindred()
                {
                    Level = SharedKernal.Enums.KindredLevels.Father,
                    PatientLeftId = newpa.Id,
                    PatientRightId = id,
                });

                await context.SaveChangesAsync();
            }

            var data =  await context.Patients.Where(x => !x.DeleteDate.HasValue).Where(p => p.Id == id).
                Select(x => new ProfileDto()
                {
                    FullName = x.FirstName + " " + x.LastName,
                    Area = x.Area.Name,
                    Career = x.Career.Name,
                    Kindreds = x.KindredLefts.Where(x => !x.DeleteDate.HasValue).
                    Select(x=> new KindredDto() {
                        Name = x.PatientRight.FirstName + " " + x.PatientRight.LastName,
                        Level  =x.Level.ToString(),
                    })
                }).FirstOrDefaultAsync();

            return new OperationResult<ProfileDto>()
            {
                OperationResultType = OperationResultTypes.Success,
                Result = data,
            };
        }
    }
}
