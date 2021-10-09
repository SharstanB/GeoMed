using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoMed.SqlServer.Migrations
{
    public partial class setDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Kindreds",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "HealthCenters",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Doctors",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DoctorReviews",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DoctorReviewDiseases",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 225, DateTimeKind.Local).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 230, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 230, DateTimeKind.Local).AddTicks(5809));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 230, DateTimeKind.Local).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 230, DateTimeKind.Local).AddTicks(5910));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 230, DateTimeKind.Local).AddTicks(9281));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 231, DateTimeKind.Local).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 231, DateTimeKind.Local).AddTicks(56));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 231, DateTimeKind.Local).AddTicks(82));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 231, DateTimeKind.Local).AddTicks(105));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 231, DateTimeKind.Local).AddTicks(132));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 231, DateTimeKind.Local).AddTicks(155));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 20, 14, 8, 231, DateTimeKind.Local).AddTicks(177));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Kindreds",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "HealthCenters",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Doctors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DoctorReviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DoctorReviewDiseases",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 181, DateTimeKind.Local).AddTicks(9595));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 195, DateTimeKind.Local).AddTicks(9808));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(379));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(433));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(470));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(4761));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(6221));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(6317));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(6469));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(6524));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 12, 13, 40, 196, DateTimeKind.Local).AddTicks(6574));
        }
    }
}
