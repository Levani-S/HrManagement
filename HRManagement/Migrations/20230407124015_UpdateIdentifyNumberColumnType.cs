using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentifyNumberColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "IdentifyNumber",
    table: "Employees",
    type: "nvarchar(11)",
    maxLength: 11,
    nullable: false,
    oldClrType: typeof(int),
    oldType: "int",
    oldMaxLength: 11);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
    name: "IdentifyNumber",
    table: "Employees",
    type: "int",
    maxLength: 11,
    nullable: false,
    oldClrType: typeof(string),
    oldType: "nvarchar(11)",
    oldMaxLength: 11);
        }
    }
}
