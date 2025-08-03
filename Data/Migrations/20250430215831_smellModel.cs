using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_eCommerce_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class smellModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SmellId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Smell",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smell", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SmellId",
                table: "Products",
                column: "SmellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Smell_SmellId",
                table: "Products",
                column: "SmellId",
                principalTable: "Smell",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Smell_SmellId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Smell");

            migrationBuilder.DropIndex(
                name: "IX_Products_SmellId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SmellId",
                table: "Products");
        }
    }
}
