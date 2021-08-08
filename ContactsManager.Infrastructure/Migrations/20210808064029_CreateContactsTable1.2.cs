using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsManager.Infrastructure.Migrations
{
    public partial class CreateContactsTable12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contactId", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
