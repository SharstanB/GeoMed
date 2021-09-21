using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoMed.SqlServer.Migrations
{
    public partial class addfibtoSpatialInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Population",
                table: "SpatialInfos",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "fib",
                table: "SpatialInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fib",
                table: "SpatialInfos");

            migrationBuilder.AlterColumn<int>(
                name: "Population",
                table: "SpatialInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
