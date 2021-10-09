using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoMed.SqlServer.Migrations
{
    public partial class patientBloodType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Patients");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 894, DateTimeKind.Local).AddTicks(3107));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9193));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9547));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9713));

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 901, DateTimeKind.Local).AddTicks(9803));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(5832));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7064));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7334));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7389));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7426));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7472));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Careers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateDate",
                value: new DateTime(2021, 10, 2, 11, 12, 14, 902, DateTimeKind.Local).AddTicks(7543));
        }
    }
}
