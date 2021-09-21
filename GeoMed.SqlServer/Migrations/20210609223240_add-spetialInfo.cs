using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoMed.SqlServer.Migrations
{
    public partial class addspetialInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "CovidZones");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "CovidZones");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "CovidZones");

            migrationBuilder.DropColumn(
                name: "State",
                table: "CovidZones");

            migrationBuilder.AddColumn<int>(
                name: "SpatialInfoId",
                table: "CovidZones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SpatialInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Long = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpatialInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CovidZones_SpatialInfoId",
                table: "CovidZones",
                column: "SpatialInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CovidZones_SpatialInfos_SpatialInfoId",
                table: "CovidZones",
                column: "SpatialInfoId",
                principalTable: "SpatialInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovidZones_SpatialInfos_SpatialInfoId",
                table: "CovidZones");

            migrationBuilder.DropTable(
                name: "SpatialInfos");

            migrationBuilder.DropIndex(
                name: "IX_CovidZones_SpatialInfoId",
                table: "CovidZones");

            migrationBuilder.DropColumn(
                name: "SpatialInfoId",
                table: "CovidZones");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CovidZones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Lat",
                table: "CovidZones",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Long",
                table: "CovidZones",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "CovidZones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
