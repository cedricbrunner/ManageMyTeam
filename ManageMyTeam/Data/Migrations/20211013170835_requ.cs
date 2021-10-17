using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageMyTeam.Data.Migrations
{
    public partial class requ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequirementHours_Employees_EmployeeId",
                table: "RequirementHours");

            migrationBuilder.DropIndex(
                name: "IX_RequirementHours_EmployeeId",
                table: "RequirementHours");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "RequirementHours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "RequirementHours",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequirementHours_EmployeeId",
                table: "RequirementHours",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequirementHours_Employees_EmployeeId",
                table: "RequirementHours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
