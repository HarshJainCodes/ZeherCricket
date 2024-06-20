using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeherCricket.Migrations
{
    /// <inheritdoc />
    public partial class addMatchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchInfoTable",
                columns: table => new
                {
                    matchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchInfoTable", x => x.matchId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchInfoTable");
        }
    }
}
