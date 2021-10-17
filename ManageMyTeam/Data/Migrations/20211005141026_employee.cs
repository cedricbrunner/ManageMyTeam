using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageMyTeam.Data.Migrations
{
    public partial class employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequirementHours_Departments_DepartmentId",
                table: "RequirementHours");

            migrationBuilder.DropIndex(
                name: "IX_RequirementHours_DepartmentId",
                table: "RequirementHours");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "RequirementHours");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "RequirementHours",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "RequirementHours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RequirementHours_DepartmentId",
                table: "RequirementHours",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequirementHours_Departments_DepartmentId",
                table: "RequirementHours",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
