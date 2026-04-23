using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldProfileIdinRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "RefreshTokens",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ProfileId",
                table: "RefreshTokens",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Profiles_ProfileId",
                table: "RefreshTokens",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Profiles_ProfileId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_ProfileId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "RefreshTokens");
        }
    }
}
