using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentManagementSystem.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employee_Salary",
                table: "AspNetUsers",
                column: "Salary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employee_Salary",
                table: "AspNetUsers");
        }
    }
}
