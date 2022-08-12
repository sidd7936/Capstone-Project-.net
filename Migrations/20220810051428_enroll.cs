using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignUpAPI.Migrations
{
    public partial class enroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EmployeeInvestment",
                table: "Sign",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EmployerInvestment",
                table: "Sign",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RetirementInvestment",
                table: "Sign",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeInvestment",
                table: "Sign");

            migrationBuilder.DropColumn(
                name: "EmployerInvestment",
                table: "Sign");

            migrationBuilder.DropColumn(
                name: "RetirementInvestment",
                table: "Sign");
        }
    }
}
