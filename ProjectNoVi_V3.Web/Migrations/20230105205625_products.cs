using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectNoVi_V3.Web.Migrations
{
    public partial class products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MerkId = table.Column<int>(type: "int", nullable: false),
                    ProductMerkId = table.Column<int>(type: "int", nullable: true),
                    Collectie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kleur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Materiaal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prijs = table.Column<int>(type: "int", nullable: false),
                    Correctie = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_ProductMerkId",
                        column: x => x.ProductMerkId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductMerkId",
                table: "Product",
                column: "ProductMerkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
