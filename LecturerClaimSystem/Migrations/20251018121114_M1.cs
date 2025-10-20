using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecturerClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Claims_ClaimId",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_ClaimId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_ClaimId");

            migrationBuilder.AddColumn<int>(
                name: "ClaimId1",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "FeedbackId");

            migrationBuilder.CreateTable(
                name: "Coordinators",
                columns: table => new
                {
                    CoordinatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinators", x => x.CoordinatorId);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ClaimId1",
                table: "Feedbacks",
                column: "ClaimId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId",
                table: "Feedbacks",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId1",
                table: "Feedbacks",
                column: "ClaimId1",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Claims_ClaimId1",
                table: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Coordinators");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ClaimId1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "ClaimId1",
                table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_ClaimId",
                table: "Feedback",
                newName: "IX_Feedback_ClaimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Claims_ClaimId",
                table: "Feedback",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
