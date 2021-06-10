using GeoMed.Model.DataSet;
using GeoMed.Model.Main;
using GeoMed.Model.Setting;
using GeoMed.Model.Templete;
using Microsoft.EntityFrameworkCore;

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

            # region == Global Query Filter ==
            #region  == Settings Filters == 

            modelBuilder.Entity<Area>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Career>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Disease>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<Symptom>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);


            #endregion

            #region  == Main Filters == 
            modelBuilder.Entity<PatientRecord>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
             modelBuilder.Entity<TrackRecord>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);

            #endregion

            #region == Templete Filters ==

            modelBuilder.Entity<Field>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);

            modelBuilder.Entity<Templete>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);


            #endregion


            #region == DbSet Default values ==

            modelBuilder.Entity<CovidZone>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<ModelSet>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);
            modelBuilder.Entity<SpatialInfo>().HasQueryFilter(patientRecord => !patientRecord.DeleteDate.HasValue);

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


            #endregion

            #region  == Main Default values == 
            modelBuilder.Entity<PatientRecord>().Property(b => b.CreateDate)
            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TrackRecord>().Property(b => b.CreateDate)
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

            modelBuilder.Entity<SpatialInfo>().Property(b => b.CreateDate)
         .HasDefaultValueSql("getdate()");

            #endregion

            #endregion 
        }

        #endregion


    }
}
