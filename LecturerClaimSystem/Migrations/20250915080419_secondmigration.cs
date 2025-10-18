using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecturerClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinatorFeedback",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ManagerFeedback",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "DocumentPath",
                table: "Claims",
                newName: "SupportingDocument");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Lecturers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "HoursWorked",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ClaimId",
                table: "Feedback",
                column: "ClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "HoursWorked",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "SupportingDocument",
                table: "Claims",
                newName: "DocumentPath");

            migrationBuilder.AddColumn<string>(
                name: "CoordinatorFeedback",
                table: "Claims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerFeedback",
                table: "Claims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
