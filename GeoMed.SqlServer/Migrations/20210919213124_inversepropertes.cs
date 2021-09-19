using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoMed.SqlServer.Migrations
{
    public partial class inversepropertes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientLeftId",
                table: "Kindreds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientRightId",
                table: "Kindreds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kindreds_PatientLeftId",
                table: "Kindreds",
                column: "PatientLeftId");

            migrationBuilder.CreateIndex(
                name: "IX_Kindreds_PatientRightId",
                table: "Kindreds",
                column: "PatientRightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kindreds_Patients_PatientLeftId",
                table: "Kindreds",
                column: "PatientLeftId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kindreds_Patients_PatientRightId",
                table: "Kindreds",
                column: "PatientRightId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kindreds_Patients_PatientLeftId",
                table: "Kindreds");

            migrationBuilder.DropForeignKey(
                name: "FK_Kindreds_Patients_PatientRightId",
                table: "Kindreds");

            migrationBuilder.DropIndex(
                name: "IX_Kindreds_PatientLeftId",
                table: "Kindreds");

            migrationBuilder.DropIndex(
                name: "IX_Kindreds_PatientRightId",
                table: "Kindreds");

            migrationBuilder.DropColumn(
                name: "PatientLeftId",
                table: "Kindreds");

            migrationBuilder.DropColumn(
                name: "PatientRightId",
                table: "Kindreds");
        }
    }
}
