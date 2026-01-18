using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_table_reservation_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "restaurant_review",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    restaurant_id = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurant_review", x => x.id);
                    table.ForeignKey(
                        name: "fk_restaurant_review_restaurant_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_restaurant_review_restaurant_id",
                table: "restaurant_review",
                column: "restaurant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "restaurant_review");
        }
    }
}
