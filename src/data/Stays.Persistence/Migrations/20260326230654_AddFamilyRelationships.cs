using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stays.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Family_Users_CreatedByUserId",
                table: "Family");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Family_FamilyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Family",
                table: "Family");

            migrationBuilder.RenameTable(
                name: "Family",
                newName: "Families");

            migrationBuilder.RenameIndex(
                name: "IX_Family_CreatedByUserId",
                table: "Families",
                newName: "IX_Families_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Families",
                table: "Families",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Users_CreatedByUserId",
                table: "Families",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Families_FamilyId",
                table: "Users",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_Users_CreatedByUserId",
                table: "Families");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Families_FamilyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Families",
                table: "Families");

            migrationBuilder.RenameTable(
                name: "Families",
                newName: "Family");

            migrationBuilder.RenameIndex(
                name: "IX_Families_CreatedByUserId",
                table: "Family",
                newName: "IX_Family_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Family",
                table: "Family",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Family_Users_CreatedByUserId",
                table: "Family",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Family_FamilyId",
                table: "Users",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
