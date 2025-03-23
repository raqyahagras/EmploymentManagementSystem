using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentManagementSystem.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class removePErmissionUpdatedByCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 120);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 140);
        }
    }
}
