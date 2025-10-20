using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecturerClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId",
                table: "Feedbacks");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId",
                table: "Feedbacks",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId",
                table: "Feedbacks");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId",
                table: "Feedbacks",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
