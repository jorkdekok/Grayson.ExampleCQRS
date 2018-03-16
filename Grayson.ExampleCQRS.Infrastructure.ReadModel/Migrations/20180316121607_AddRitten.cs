using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Grayson.ExampleCQRS.Infrastructure.ReadModel.Migrations
{
    public partial class AddRitten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ritten",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BeginStand = table.Column<int>(nullable: false),
                    BeginStandId = table.Column<Guid>(nullable: false),
                    EindStand = table.Column<int>(nullable: false),
                    EindStandId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ritten", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ritten");
        }
    }
}
