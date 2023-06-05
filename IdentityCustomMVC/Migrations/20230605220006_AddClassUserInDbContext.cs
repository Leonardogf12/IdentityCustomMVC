using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityCustomMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddClassUserInDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "USR_ADDRESS",
                table: "AspNetUsers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "USR_BIRTH_DATE",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USR_CELL_PHONE",
                table: "AspNetUsers",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "USR_CEP",
                table: "AspNetUsers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "USR_CPF",
                table: "AspNetUsers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "USR_NAME",
                table: "AspNetUsers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "USR_STATUS",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "USR_ADDRESS",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "USR_BIRTH_DATE",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "USR_CELL_PHONE",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "USR_CEP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "USR_CPF",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "USR_NAME",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "USR_STATUS",
                table: "AspNetUsers");
        }
    }
}
