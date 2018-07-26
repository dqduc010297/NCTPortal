using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructures.DbMigration.Migrations
{
    public partial class update_Username_ProblemMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemMessage_Shipment_ShipmentId1",
                table: "ProblemMessage");

            migrationBuilder.RenameColumn(
                name: "ShipmentId1",
                table: "ProblemMessage",
                newName: "RequestId1");

            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "ProblemMessage",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_ProblemMessage_ShipmentId1",
                table: "ProblemMessage",
                newName: "IX_ProblemMessage_RequestId1");

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "ShipmentRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSolve",
                table: "ProblemMessage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemMessage_Request_RequestId1",
                table: "ProblemMessage",
                column: "RequestId1",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemMessage_Request_RequestId1",
                table: "ProblemMessage");

            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "ShipmentRequest");

            migrationBuilder.DropColumn(
                name: "IsSolve",
                table: "ProblemMessage");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RequestId1",
                table: "ProblemMessage",
                newName: "ShipmentId1");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "ProblemMessage",
                newName: "ShipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProblemMessage_RequestId1",
                table: "ProblemMessage",
                newName: "IX_ProblemMessage_ShipmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemMessage_Shipment_ShipmentId1",
                table: "ProblemMessage",
                column: "ShipmentId1",
                principalTable: "Shipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
