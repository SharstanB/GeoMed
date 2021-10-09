using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoMed.SqlServer.Migrations
{
    public partial class seedAreaCareer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TrackRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Templetes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Symptoms",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "SpatialInfos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PatientRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Models",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Fields",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Diseases",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "CovidZones",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Careers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Areas",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 2, 11, 12, 14, 894, DateTimeKind.Local).AddTicks(3107), null, "حلب - جميلية" },
                    { 2, new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9193), null, "حلب - سوق أنتاج" },
                    { 3, new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9547), null, "حلب - محافظة" },
                    { 4, new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9713), null, "دمشق - سيدة زينب" },
                    { 5, new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9803), null, " حلب - عفرين" }
                });

            migrationBuilder.InsertData(
                table: "Careers",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(5832), null, " عامل تمديدات الصحية" },
                    { 2, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7064), null, "نجار" },
                    { 3, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7334), null, " كهربة السيارات" },
                    { 4, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7389), null, "معلم" },
                    { 5, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7426), null, "شرطي مرور" },
                    { 6, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7472), null, "حارس" },
                    { 7, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7508), null, "طبيب أسنان" },
                    { 8, new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7543), null, "نادل المطعم" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TrackRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Templetes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Symptoms",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "SpatialInfos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PatientRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Models",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Fields",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Diseases",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "CovidZones",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Careers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Areas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
