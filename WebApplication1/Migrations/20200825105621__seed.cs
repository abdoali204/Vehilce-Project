using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class _seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) Values ('Make1')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) Values ('Make2')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) Values ('Make3')");

            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make1-ModelA',(SELECT ID FROM Makes WHERE Name= 'Make1'))");
            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make1-ModelB',(SELECT ID FROM Makes WHERE Name= 'Make1'))");
            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make1-ModelC',(SELECT ID FROM Makes WHERE Name= 'Make1'))");


            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make2-ModelA',(Select ID From Makes Where Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make2-ModelB',(SELECT ID FROM Makes WHERE Name= 'Make2' ))");
            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make2-ModelC',(SELECT ID FROM Makes WHERE Name= 'Make2' ))");




            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make3-ModelA',(Select ID From Makes Where Name = 'Make3'))");
            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make3-ModelB',(SELECT ID FROM Makes WHERE Name= 'Make3' ))");
            migrationBuilder.Sql("INSERT INTO Model (Name,MakeId) Values ('Make3-ModelC',(SELECT ID FROM Makes WHERE Name= 'Make3' ))");





        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM Models");
            migrationBuilder.Sql("Delete FROM Makes WHERE Name IN ('Make1','Make2','Make3')");

        }
    }
}
