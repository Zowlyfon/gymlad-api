using Microsoft.EntityFrameworkCore.Migrations;

namespace GymLad.Migrations
{
    public partial class username : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "People",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "People",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "People",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "People",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "People",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "People",
                newName: "height");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "People",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "People",
                newName: "id");
        }
    }
}
