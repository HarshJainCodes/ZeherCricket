using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeherCricket.Migrations
{
    /// <inheritdoc />
    public partial class addmatchnumberonuserselection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "matchId",
                table: "UserMatchSelectionTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "matchId",
                table: "UserMatchSelectionTable");
        }
    }
}
