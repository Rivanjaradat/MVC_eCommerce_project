using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_eCommerce_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class smellModelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Smell_SmellId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Smell",
                table: "Smell");

            migrationBuilder.DropColumn(
                name: "Smell",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Smell",
                newName: "Smells");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Smells",
                table: "Smells",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Smells_SmellId",
                table: "Products",
                column: "SmellId",
                principalTable: "Smells",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Smells_SmellId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Smells",
                table: "Smells");

            migrationBuilder.RenameTable(
                name: "Smells",
                newName: "Smell");

            migrationBuilder.AddColumn<string>(
                name: "Smell",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Smell",
                table: "Smell",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Smell_SmellId",
                table: "Products",
                column: "SmellId",
                principalTable: "Smell",
                principalColumn: "Id");
        }
    }
}
