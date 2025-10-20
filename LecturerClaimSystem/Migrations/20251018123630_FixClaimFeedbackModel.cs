using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecturerClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixClaimFeedbackModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Lecturers_LecturerId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId1",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ClaimId1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "ClaimId1",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Claims",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Lecturers_LecturerId",
                table: "Claims",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Lecturers_LecturerId",
                table: "Claims");

            migrationBuilder.AddColumn<int>(
                name: "ClaimId1",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Claims",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Claims",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ClaimId1",
                table: "Feedbacks",
                column: "ClaimId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Lecturers_LecturerId",
                table: "Claims",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId1",
                table: "Feedbacks",
                column: "ClaimId1",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
