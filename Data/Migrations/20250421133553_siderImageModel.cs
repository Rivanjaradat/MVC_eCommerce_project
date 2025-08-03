using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_eCommerce_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class siderImageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "SliderImages");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SliderImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SliderImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SliderImages");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SliderImages");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "SliderImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
