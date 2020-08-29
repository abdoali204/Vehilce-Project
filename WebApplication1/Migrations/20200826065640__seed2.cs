using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class _seed2 : Migration
    {
      protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features Values('Feature1')");
            migrationBuilder.Sql("INSERT INTO Features Values('Feature2')");
            migrationBuilder.Sql("INSERT INTO Features Values('Feature3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.Sql("Delete FROM Makes WHERE Name IN ('Feature1','Feature2','Feature3')");

        }
    }
}
