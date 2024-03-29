﻿using GeoMed.Model.DataSet;
using GeoMed.Model.Main;
using GeoMed.Model.Setting;
using GeoMed.Model.Templete;
using Microsoft.EntityFrameworkCore;
using System;

namespace GeoMed.SqlServer
{
    public class GMContext  : DbContext   
    {
        #region == Constructer ==

        public GMContext(DbContextOptions<GMContext> options)
           : base(options)
        {

        }
        #endregion

        #region  == Settings Entities == 

        public DbSet<Area> Areas { get; set; }

        public DbSet<Career> Careers { get; set; }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<Kindred> Kindreds { get; set; }


        public DbSet<HealthCenter> HealthCenters { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<DoctorReview> DoctorReviews { get; set; }

        public DbSet<DoctorReviewDisease> DoctorReviewDiseases { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        #endregion

        #region  == Main Entities == 
        public DbSet<PatientRecord> PatientRecords { get; set; }
        public DbSet<TrackRecord> TrackRecords { get; set; }

        public DbSet<Patient> Patients { get; set; }

#endregion

        #region == Templete Entities ==

        public DbSet<Field> Fields { get; set; }

        public DbSet<Templete> Templetes { get; set; }


        #endregion

        #region == DbSet ==

        public DbSet<CovidZone> CovidZones { get; set; }

        public DbSet<ModelSet> Models { get; set; }

        public DbSet<SpatialInfo> SpatialInfos { get; set; }
        #endregion


        #region == Methods == 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


          // modelBuilder.Entity<Patient>()
          //.HasMany(a => a.KindredLefts)
          //.WithOne(b => b.PatientLeft)
          //.HasForeignKey(b=> b.PatientLeftId)
          //.OnDelete(DeleteBehavior.Cascade);



            # region == Global Query Filter ==
            #region  == Settings Filters == 

            modelBuilder.Entity<Area>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Career>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Disease>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Symptom>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<HealthCenter>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Kindred>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Notification>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);


            #endregion

            #region  == Main Filters == 
            modelBuilder.Entity<PatientRecord>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Patient>().HasQueryFilter(patient => !patient.DeleteDate.HasValue);
            modelBuilder.Entity<TrackRecord>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);

            #endregion

            #region == Templete Filters ==

            modelBuilder.Entity<Field>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);

            modelBuilder.Entity<Templete>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);


            #endregion


            #region == DbSet Default values ==

            modelBuilder.Entity<CovidZone>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Review>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<ModelSet>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<SpatialInfo>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Doctor>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Chat>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<DoctorReviewDisease>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<DoctorReview>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);

            #endregion

            #endregion

            #region == Set Default values ==
            #region  == Settings Default values == 

            modelBuilder.Entity<Area>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Career>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Disease>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Symptom>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");

             modelBuilder.Entity<Notification>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");


            #endregion

            #region  == Main Default values == 
            modelBuilder.Entity<PatientRecord>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TrackRecord>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()"); 
            
            modelBuilder.Entity<Chat>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()"); 
            
            modelBuilder.Entity<Doctor>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()"); 
            
            modelBuilder.Entity<DoctorReview>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()"); 

             modelBuilder.Entity<Review>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()"); 
            
            modelBuilder.Entity<DoctorReviewDisease>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()"); 
            
            modelBuilder.Entity<HealthCenter>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");
            
            modelBuilder.Entity<Kindred>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");
            

            #endregion

            #region == Templete Default values ==

            modelBuilder.Entity<Field>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Templete>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");


            #endregion


            #region == DbSet Default values ==

            modelBuilder.Entity<CovidZone>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ModelSet>().Property(b => b.CreateDate)
           .HasDefaultValueSql("getdate()");
            
            modelBuilder.Entity<Patient>().Property(b => b.CreateDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<SpatialInfo>().Property(b => b.CreateDate)
         .HasDefaultValueSql("getdate()");

            #endregion
            #region Areas
            modelBuilder.Entity<Area>().HasData(new Area
            {
                Id = 1,
                CreateDate = DateTime.Now,
                Name = "حلب - جميلية"
            }); modelBuilder.Entity<Area>().HasData(new Area
            {
                Id = 2,
                CreateDate = DateTime.Now,
                Name = "حلب - سوق أنتاج"
            }); modelBuilder.Entity<Area>().HasData(new Area
            {
                Id = 3,
                CreateDate = DateTime.Now,
                Name = "حلب - محافظة"
            }); modelBuilder.Entity<Area>().HasData(new Area
            {
                Id = 4,
                CreateDate = DateTime.Now,
                Name = "دمشق - سيدة زينب"
            }); modelBuilder.Entity<Area>().HasData(new Area
            {
                Id = 5,
                CreateDate = DateTime.Now,
                Name = " حلب - عفرين"
            });

            #endregion

            #region Career
            modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 1,
                CreateDate = DateTime.Now,
                Name = " عامل تمديدات الصحية"
            }); modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 2,
                CreateDate = DateTime.Now,
                Name = "نجار"
            }); modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 3,
                CreateDate = DateTime.Now,
                Name = " كهربة السيارات"
            }); modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 4,
                CreateDate = DateTime.Now,
                Name = "معلم"
            });modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 5,
                CreateDate = DateTime.Now,
                Name = "شرطي مرور"
            });modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 6,
                CreateDate = DateTime.Now,
                Name = "حارس"
            });modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 7,
                CreateDate = DateTime.Now,
                Name = "طبيب أسنان"
            });modelBuilder.Entity<Career>().HasData(new Career
            {
                Id = 8,
                CreateDate = DateTime.Now,
                Name = "نادل المطعم"
            });


            #endregion




            #endregion
        }

        #endregion


    }
}
