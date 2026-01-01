using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_ComplaintSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Complaints",
                newName: "ComplaintId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Complaints",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Complaints",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Complaints",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComplaintId",
                table: "Complaints",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Complaints",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Complaints",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Complaints",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
