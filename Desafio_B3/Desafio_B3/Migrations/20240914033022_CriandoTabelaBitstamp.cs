using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_B3.Migrations
{
    public partial class CriandoTabelaBitstamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bitstamps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ask = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Last = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Open24 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentChange24 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Side = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vwap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitstamps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bitstamps");
        }
    }
}
