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

            #endregion

        }

        #endregion





    }
}
