﻿// <auto-generated />
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
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

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
                        .UseIdentityColumn();

                    b.Property<double>("Cases")
                        .HasColumnType("float");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FipsCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Long")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CovidZones");
                });

            modelBuilder.Entity("GeoMed.Model.Main.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CareerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CareerId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("GeoMed.Model.Main.PatientRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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

            modelBuilder.Entity("GeoMed.Model.Main.TrackRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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
                        .UseIdentityColumn();

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
                        .UseIdentityColumn();

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
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("GeoMed.Model.Setting.Symptom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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
                        .UseIdentityColumn();

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
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
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

            modelBuilder.Entity("GeoMed.Model.Main.Patient", b =>
                {
                    b.HasOne("GeoMed.Model.Setting.Area", "Area")
                        .WithMany("Patients")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeoMed.Model.Setting.Career", null)
                        .WithMany("Patients")
                        .HasForeignKey("CareerId");

                    b.Navigation("Area");
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

            modelBuilder.Entity("GeoMed.Model.Main.TrackRecord", b =>
                {
                    b.HasOne("GeoMed.Model.Main.PatientRecord", "PatientRecord")
                        .WithMany("TrackRecords")
                        .HasForeignKey("PatientRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientRecord");
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

            modelBuilder.Entity("GeoMed.Model.Main.Patient", b =>
                {
                    b.Navigation("PatientRecords");
                });

            modelBuilder.Entity("GeoMed.Model.Main.PatientRecord", b =>
                {
                    b.Navigation("TrackRecords");
                });

            modelBuilder.Entity("GeoMed.Model.Main.TrackRecord", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("GeoMed.Model.Setting.Area", b =>
                {
                    b.Navigation("Patients");
                });

            modelBuilder.Entity("GeoMed.Model.Setting.Career", b =>
                {
                    b.Navigation("Patients");
                });

            modelBuilder.Entity("GeoMed.Model.Templete.Templete", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}
