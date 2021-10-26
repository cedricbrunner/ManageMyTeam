using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageMyTeam.Data.Migrations
{
    public partial class siite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "PublicHolidays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PublicHolidays_SiteId",
                table: "PublicHolidays",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicHolidays_Sites_SiteId",
                table: "PublicHolidays",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicHolidays_Sites_SiteId",
                table: "PublicHolidays");

            migrationBuilder.DropIndex(
                name: "IX_PublicHolidays_SiteId",
                table: "PublicHolidays");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "PublicHolidays");
        }
    }
}
