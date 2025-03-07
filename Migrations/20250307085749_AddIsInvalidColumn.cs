using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentInfoChatBot.Migrations
{
    /// <inheritdoc />
    public partial class AddIsInvalidColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvalid",
                table: "StudentQueries",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvalid",
                table: "StudentQueries");
        }
    }
}
