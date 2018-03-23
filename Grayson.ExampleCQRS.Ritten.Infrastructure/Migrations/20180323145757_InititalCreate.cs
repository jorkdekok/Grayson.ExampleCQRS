using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.Migrations
{
    public partial class InititalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KmStand",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdresId = table.Column<Guid>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Stand = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KmStand", x => x.Id);
                });

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
                name: "KmStand");

            migrationBuilder.DropTable(
                name: "Ritten");
        }
    }
}
