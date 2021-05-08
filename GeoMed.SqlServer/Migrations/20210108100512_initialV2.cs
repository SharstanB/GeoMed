using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoMed.SqlServer.Migrations
{
    public partial class initialV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GMUserId",
                table: "PatientRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrackRecordId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_GMUserId",
                table: "PatientRecords",
                column: "GMUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_TrackRecordId",
                table: "Fields",
                column: "TrackRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_TrackRecords_TrackRecordId",
                table: "Fields",
                column: "TrackRecordId",
                principalTable: "TrackRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecords_AspNetUsers_GMUserId",
                table: "PatientRecords",
                column: "GMUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_TrackRecords_TrackRecordId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecords_AspNetUsers_GMUserId",
                table: "PatientRecords");

            migrationBuilder.DropIndex(
                name: "IX_PatientRecords_GMUserId",
                table: "PatientRecords");

            migrationBuilder.DropIndex(
                name: "IX_Fields_TrackRecordId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "GMUserId",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "TrackRecordId",
                table: "Fields");
        }
    }
}
