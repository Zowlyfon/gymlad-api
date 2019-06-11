using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymLad.Migrations
{
    public partial class auth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "People",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "People",
                newName: "SecurityStamp");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "People",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "People",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "People",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "People",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "People");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "People");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "People");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "People");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "People",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "People",
                newName: "Password");
        }
    }
}
