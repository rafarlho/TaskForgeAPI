using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedTypoOnDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskGroups_Organizaions_OrganizationId",
                table: "TaskGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizaions",
                table: "Organizaions");

            migrationBuilder.RenameTable(
                name: "Organizaions",
                newName: "Organizations");

            migrationBuilder.RenameIndex(
                name: "IX_Organizaions_Name",
                table: "Organizations",
                newName: "IX_Organizations_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskGroups_Organizations_OrganizationId",
                table: "TaskGroups",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskGroups_Organizations_OrganizationId",
                table: "TaskGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Organizaions");

            migrationBuilder.RenameIndex(
                name: "IX_Organizations_Name",
                table: "Organizaions",
                newName: "IX_Organizaions_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizaions",
                table: "Organizaions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskGroups_Organizaions_OrganizationId",
                table: "TaskGroups",
                column: "OrganizationId",
                principalTable: "Organizaions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
