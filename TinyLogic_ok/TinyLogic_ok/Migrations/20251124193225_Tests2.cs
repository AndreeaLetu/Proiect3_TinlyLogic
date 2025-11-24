using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyLogic_ok.Migrations
{
    /// <inheritdoc />
    public partial class Tests2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestProgress_AspNetUsers_UserId",
                table: "TestProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_TestProgress_Tests_TestId",
                table: "TestProgress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestProgress",
                table: "TestProgress");

            migrationBuilder.RenameTable(
                name: "TestProgress",
                newName: "TestProgresses");

            migrationBuilder.RenameIndex(
                name: "IX_TestProgress_UserId",
                table: "TestProgresses",
                newName: "IX_TestProgresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestProgress_TestId",
                table: "TestProgresses",
                newName: "IX_TestProgresses_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestProgresses",
                table: "TestProgresses",
                column: "IdTestProgress");

            migrationBuilder.AddForeignKey(
                name: "FK_TestProgresses_AspNetUsers_UserId",
                table: "TestProgresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestProgresses_Tests_TestId",
                table: "TestProgresses",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "IdTest",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestProgresses_AspNetUsers_UserId",
                table: "TestProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_TestProgresses_Tests_TestId",
                table: "TestProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestProgresses",
                table: "TestProgresses");

            migrationBuilder.RenameTable(
                name: "TestProgresses",
                newName: "TestProgress");

            migrationBuilder.RenameIndex(
                name: "IX_TestProgresses_UserId",
                table: "TestProgress",
                newName: "IX_TestProgress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestProgresses_TestId",
                table: "TestProgress",
                newName: "IX_TestProgress_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestProgress",
                table: "TestProgress",
                column: "IdTestProgress");

            migrationBuilder.AddForeignKey(
                name: "FK_TestProgress_AspNetUsers_UserId",
                table: "TestProgress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestProgress_Tests_TestId",
                table: "TestProgress",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "IdTest",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
