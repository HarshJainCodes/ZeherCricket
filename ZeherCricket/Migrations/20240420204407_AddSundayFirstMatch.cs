using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeherCricket.Migrations
{
    /// <inheritdoc />
    public partial class AddSundayFirstMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSundayFirstMatch",
                table: "MatchInfoTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSundayFirstMatch",
                table: "MatchInfoTable");
        }
    }
}
