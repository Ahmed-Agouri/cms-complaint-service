using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComplaintModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Complaints",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Complaints",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }
    }
}
