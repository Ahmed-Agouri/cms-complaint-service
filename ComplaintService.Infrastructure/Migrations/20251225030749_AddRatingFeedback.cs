using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Complaints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Complaints",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Complaints");
        }
    }
}
