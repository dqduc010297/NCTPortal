using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructures.DbMigration.Migrations
{
    public partial class updateProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemMessage_Request_RequestId1",
                table: "ProblemMessage");

            migrationBuilder.DropIndex(
                name: "IX_ProblemMessage_RequestId1",
                table: "ProblemMessage");

            migrationBuilder.DropColumn(
                name: "RequestId1",
                table: "ProblemMessage");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "ProblemMessage",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_ProblemMessage_RequestId",
                table: "ProblemMessage",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemMessage_Request_RequestId",
                table: "ProblemMessage",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemMessage_Request_RequestId",
                table: "ProblemMessage");

            migrationBuilder.DropIndex(
                name: "IX_ProblemMessage_RequestId",
                table: "ProblemMessage");

            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "ProblemMessage",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RequestId1",
                table: "ProblemMessage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProblemMessage_RequestId1",
                table: "ProblemMessage",
                column: "RequestId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemMessage_Request_RequestId1",
                table: "ProblemMessage",
                column: "RequestId1",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
