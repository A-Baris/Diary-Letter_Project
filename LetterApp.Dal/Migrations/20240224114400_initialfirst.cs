using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetterApp.Dal.Migrations
{
    /// <inheritdoc />
    public partial class initialfirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordConfirmed = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    City = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaryNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    ReceiverEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryNotes_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_UserId",
                table: "Diaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryNotes_DiaryId",
                table: "DiaryNotes",
                column: "DiaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaryNotes");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Diaries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
