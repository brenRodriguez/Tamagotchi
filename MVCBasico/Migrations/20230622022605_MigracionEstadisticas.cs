using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagochi.Migrations
{
    public partial class MigracionEstadisticas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TiempoDeCreacion",
                table: "Mascota",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Estadistica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MascotaId = table.Column<int>(type: "int", nullable: false),
                    TiempoHambrento = table.Column<long>(type: "bigint", nullable: false),
                    TiempoDebil = table.Column<long>(type: "bigint", nullable: false),
                    UltimaActualizacion = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estadistica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estadistica_Mascota_MascotaId",
                        column: x => x.MascotaId,
                        principalTable: "Mascota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estadistica_MascotaId",
                table: "Estadistica",
                column: "MascotaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estadistica");

            migrationBuilder.DropColumn(
                name: "TiempoDeCreacion",
                table: "Mascota");
        }
    }
}
