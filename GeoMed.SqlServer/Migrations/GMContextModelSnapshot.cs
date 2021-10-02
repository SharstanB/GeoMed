using System;
using GeoMed.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GeoMed.SqlServer.Migrations
{
    [DbContext(typeof(GMContext))]
    partial class GMContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DiseaseSymptom", b =>
            {
                b.Property<int>("DiseasesId")
                    .HasColumnType("int");

                b.Property<int>("SymptomsId")
                    .HasColumnType("int");

                b.HasKey("DiseasesId", "SymptomsId");

                b.HasIndex("SymptomsId");

                b.ToTable("DiseaseSymptom");
            });

            modelBuilder.Entity("GeoMed.Model.DataSet.CovidZone", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<double>("Cases")
                    .HasColumnType("float");

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("Date")
                    .HasColumnType("datetime2");

                b.Property<int>("Deaths")
                    .HasColumnType("int");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("FipsCode")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("SpatialInfoId")
                    .HasColumnType("int");

                b.Property<string>("StateCode")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("SpatialInfoId");

                b.ToTable("CovidZones");
            });

            modelBuilder.Entity("GeoMed.Model.DataSet.ModelSet", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("AlgorithmType")
                    .HasColumnType("int");

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<double>("ErrorRate")
                    .HasColumnType("float");

                b.Property<int>("ExecutedDataType")
                    .HasColumnType("int");

                b.Property<string>("Path")
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.ToTable("Models");
            });

            modelBuilder.Entity("GeoMed.Model.DataSet.SpatialInfo", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Country")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<decimal>("Lat")
                    .HasColumnType("decimal(18,2)");

                b.Property<decimal>("Long")
                    .HasColumnType("decimal(18,2)");

                b.Property<double>("MedianAge")
                    .HasColumnType("float");

                b.Property<double>("Population")
                    .HasColumnType("float");

                b.Property<string>("State")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("fib")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("SpatialInfos");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Chat", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("Date")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("DoctorId")
                    .HasColumnType("int");

                b.Property<bool>("HasSeen")
                    .HasColumnType("bit");

                b.Property<string>("Message")
                    .HasColumnType("nvarchar(max)");

                b.Property<int?>("PatientId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("DoctorId");

                b.HasIndex("PatientId");

                b.ToTable("Chats");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Doctor", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("CareerId")
                    .HasColumnType("int");

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("CareerId");

                b.ToTable("Doctors");
            });

            modelBuilder.Entity("GeoMed.Model.Main.DoctorReview", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("DoctorId")
                    .HasColumnType("int");

                b.Property<string>("Recipe")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("ReviewId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("DoctorId");

                b.HasIndex("ReviewId");

                b.ToTable("DoctorReviews");
            });

            modelBuilder.Entity("GeoMed.Model.Main.DoctorReviewDisease", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int>("DiseaseId")
                    .HasColumnType("int");

                b.Property<int>("DoctorReviewId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("DiseaseId");

                b.HasIndex("DoctorReviewId");

                b.ToTable("DoctorReviewDiseases");
            });

            modelBuilder.Entity("GeoMed.Model.Main.HealthCenter", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("AreaId")
                    .HasColumnType("int");

                b.Property<TimeSpan?>("ClosingTime")
                    .HasColumnType("time");

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<decimal?>("Lat")
                    .HasColumnType("decimal(18,2)");

                b.Property<decimal?>("Log")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<TimeSpan?>("OpeningTime")
                    .HasColumnType("time");

                b.Property<int>("Type")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("AreaId");

                b.ToTable("HealthCenters");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Kindred", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int>("Level")
                    .HasColumnType("int");

                b.Property<int?>("PatientLeftId")
                    .HasColumnType("int");

                b.Property<int?>("PatientRightId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("PatientLeftId");

                b.HasIndex("PatientRightId");

                b.ToTable("Kindreds");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Patient", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("AreaId")
                    .HasColumnType("int");

                b.Property<DateTime>("Birthdate")
                    .HasColumnType("datetime2");

                b.Property<int>("CareerId")
                    .HasColumnType("int");

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("FirstName")
                    .HasColumnType("nvarchar(50)");

                b.Property<int>("Gender")
                    .HasColumnType("int");

                b.Property<string>("LastName")
                    .HasColumnType("nvarchar(50)");

                b.Property<int?>("PatientId")
                    .HasColumnType("int");

                b.Property<int>("UserType")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("AreaId");

                b.HasIndex("CareerId");

                b.HasIndex("PatientId");

                b.ToTable("Patients");
            });

            modelBuilder.Entity("GeoMed.Model.Main.PatientRecord", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int>("DiseaseID")
                    .HasColumnType("int");

                b.Property<DateTime>("InComingDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("OutComingDate")
                    .HasColumnType("datetime2");

                b.Property<int>("PatientId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("DiseaseID");

                b.HasIndex("PatientId");

                b.ToTable("PatientRecords");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Review", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("Date")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int>("HealthCenterId")
                    .HasColumnType("int");

                b.Property<DateTime>("NextReviewDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Note")
                    .HasColumnType("nvarchar(max)");

                b.Property<int?>("PatientId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("HealthCenterId");

                b.HasIndex("PatientId");

                b.ToTable("Reviews");
            });

            modelBuilder.Entity("GeoMed.Model.Main.TrackRecord", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int>("PatientRecordId")
                    .HasColumnType("int");

                b.Property<DateTime>("PreviewDate")
                    .HasColumnType("datetime2");

                b.HasKey("Id");

                b.HasIndex("PatientRecordId");

                b.ToTable("TrackRecords");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Area", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Id");

                b.ToTable("Areas");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Career", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Id");

                b.ToTable("Careers");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Disease", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("DiseaseId")
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Id");

                b.HasIndex("DiseaseId");

                b.ToTable("Diseases");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Notification", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("DiseaseId")
                    .HasColumnType("int");

                b.Property<string>("Text")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("DiseaseId");

                b.ToTable("Notifications");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Symptom", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Id");

                b.ToTable("Symptoms");
            });

            modelBuilder.Entity("GeoMed.Model.Templete.Field", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<int>("FieldType")
                    .HasColumnType("int");

                b.Property<int>("TempleteId")
                    .HasColumnType("int");

                b.Property<int>("TrackRecordId")
                    .HasColumnType("int");

                b.Property<string>("Value")
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.HasIndex("TempleteId");

                b.HasIndex("TrackRecordId");

                b.ToTable("Fields");
            });

            modelBuilder.Entity("GeoMed.Model.Templete.Templete", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("CreateDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeleteDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Path")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Templetes");
            });

            modelBuilder.Entity("DiseaseSymptom", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Disease", null)
                    .WithMany()
                    .HasForeignKey("DiseasesId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Setting.Symptom", null)
                    .WithMany()
                    .HasForeignKey("SymptomsId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("GeoMed.Model.DataSet.CovidZone", b =>
            {
                b.HasOne("GeoMed.Model.DataSet.SpatialInfo", "SpatialInfo")
                    .WithMany("CovidZones")
                    .HasForeignKey("SpatialInfoId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("SpatialInfo");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Chat", b =>
            {
                b.HasOne("GeoMed.Model.Main.Doctor", "Doctor")
                    .WithMany("Chats")
                    .HasForeignKey("DoctorId");

                b.HasOne("GeoMed.Model.Main.Patient", "Patient")
                    .WithMany("Chats")
                    .HasForeignKey("PatientId");

                b.Navigation("Doctor");

                b.Navigation("Patient");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Doctor", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Career", "Career")
                    .WithMany("Doctors")
                    .HasForeignKey("CareerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Career");
            });

            modelBuilder.Entity("GeoMed.Model.Main.DoctorReview", b =>
            {
                b.HasOne("GeoMed.Model.Main.Doctor", "Doctor")
                    .WithMany("DoctorReviews")
                    .HasForeignKey("DoctorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Main.Review", "Review")
                    .WithMany("DoctorReviews")
                    .HasForeignKey("ReviewId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Doctor");

                b.Navigation("Review");
            });

            modelBuilder.Entity("GeoMed.Model.Main.DoctorReviewDisease", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Disease", "Disease")
                    .WithMany("DoctorReviewDiseases")
                    .HasForeignKey("DiseaseId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Main.DoctorReview", "DoctorReview")
                    .WithMany("DoctorReviewDiseases")
                    .HasForeignKey("DoctorReviewId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Disease");

                b.Navigation("DoctorReview");
            });

            modelBuilder.Entity("GeoMed.Model.Main.HealthCenter", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Area", "Area")
                    .WithMany("HealthCenters")
                    .HasForeignKey("AreaId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Area");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Kindred", b =>
            {
                b.HasOne("GeoMed.Model.Main.Patient", "PatientLeft")
                    .WithMany("KindredLefts")
                    .HasForeignKey("PatientLeftId");

                b.HasOne("GeoMed.Model.Main.Patient", "PatientRight")
                    .WithMany("KindredRights")
                    .HasForeignKey("PatientRightId");

                b.Navigation("PatientLeft");

                b.Navigation("PatientRight");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Patient", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Area", "Area")
                    .WithMany("Patients")
                    .HasForeignKey("AreaId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Setting.Career", "Career")
                    .WithMany("Patients")
                    .HasForeignKey("CareerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Main.Patient", null)
                    .WithMany("Patients")
                    .HasForeignKey("PatientId");

                b.Navigation("Area");

                b.Navigation("Career");
            });

            modelBuilder.Entity("GeoMed.Model.Main.PatientRecord", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Disease", "Disease")
                    .WithMany()
                    .HasForeignKey("DiseaseID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Main.Patient", "Patient")
                    .WithMany("PatientRecords")
                    .HasForeignKey("PatientId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Disease");

                b.Navigation("Patient");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Review", b =>
            {
                b.HasOne("GeoMed.Model.Main.HealthCenter", "HealthCenter")
                    .WithMany("Reviews")
                    .HasForeignKey("HealthCenterId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Main.Patient", "Patient")
                    .WithMany("Reviews")
                    .HasForeignKey("PatientId");

                b.Navigation("HealthCenter");

                b.Navigation("Patient");
            });

            modelBuilder.Entity("GeoMed.Model.Main.TrackRecord", b =>
            {
                b.HasOne("GeoMed.Model.Main.PatientRecord", "PatientRecord")
                    .WithMany("TrackRecords")
                    .HasForeignKey("PatientRecordId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("PatientRecord");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Disease", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Disease", null)
                    .WithMany("Diseases")
                    .HasForeignKey("DiseaseId");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Notification", b =>
            {
                b.HasOne("GeoMed.Model.Setting.Disease", "Disease")
                    .WithMany("Notifications")
                    .HasForeignKey("DiseaseId");

                b.Navigation("Disease");
            });

            modelBuilder.Entity("GeoMed.Model.Templete.Field", b =>
            {
                b.HasOne("GeoMed.Model.Templete.Templete", "Templete")
                    .WithMany("Fields")
                    .HasForeignKey("TempleteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("GeoMed.Model.Main.TrackRecord", "TrackRecord")
                    .WithMany("Fields")
                    .HasForeignKey("TrackRecordId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Templete");

                b.Navigation("TrackRecord");
            });

            modelBuilder.Entity("GeoMed.Model.DataSet.SpatialInfo", b =>
            {
                b.Navigation("CovidZones");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Doctor", b =>
            {
                b.Navigation("Chats");

                b.Navigation("DoctorReviews");
            });

            modelBuilder.Entity("GeoMed.Model.Main.DoctorReview", b =>
            {
                b.Navigation("DoctorReviewDiseases");
            });

            modelBuilder.Entity("GeoMed.Model.Main.HealthCenter", b =>
            {
                b.Navigation("Reviews");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Patient", b =>
            {
                b.Navigation("Chats");

                b.Navigation("KindredLefts");

                b.Navigation("KindredRights");

                b.Navigation("PatientRecords");

                b.Navigation("Patients");

                b.Navigation("Reviews");
            });

            modelBuilder.Entity("GeoMed.Model.Main.PatientRecord", b =>
            {
                b.Navigation("TrackRecords");
            });

            modelBuilder.Entity("GeoMed.Model.Main.Review", b =>
            {
                b.Navigation("DoctorReviews");
            });

            modelBuilder.Entity("GeoMed.Model.Main.TrackRecord", b =>
            {
                b.Navigation("Fields");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Area", b =>
            {
                b.Navigation("HealthCenters");

                b.Navigation("Patients");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Career", b =>
            {
                b.Navigation("Doctors");

                b.Navigation("Patients");
            });

            modelBuilder.Entity("GeoMed.Model.Setting.Disease", b =>
            {
                b.Navigation("Diseases");

                b.Navigation("DoctorReviewDiseases");

                b.Navigation("Notifications");
            });

            modelBuilder.Entity("GeoMed.Model.Templete.Templete", b =>
            {
                b.Navigation("Fields");
            });
#pragma warning restore 612, 618
        }
    }
}