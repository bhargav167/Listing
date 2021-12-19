using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListingApi.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ModOfRegistration = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Otp = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "masterList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CanDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masterList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_masterList_AuthUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AuthUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "listItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    HostName = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    PathName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    SearchParams = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    MasterListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_listItem_masterList_MasterListId",
                        column: x => x.MasterListId,
                        principalTable: "masterList",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_listItem_AuthUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AuthUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_listItem_MasterListId",
                table: "listItem",
                column: "MasterListId");

            migrationBuilder.CreateIndex(
                name: "IX_listItem_UserId",
                table: "listItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_masterList_UserId",
                table: "masterList",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "listItem");

            migrationBuilder.DropTable(
                name: "masterList");

            migrationBuilder.DropTable(
                name: "AuthUsers");
        }
    }
}
