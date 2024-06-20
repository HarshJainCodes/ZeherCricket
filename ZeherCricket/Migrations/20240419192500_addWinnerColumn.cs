using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeherCricket.Migrations
{
    /// <inheritdoc />
    public partial class addWinnerColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "MatchInfoTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "MatchInfoTable");
        }
    }
}
