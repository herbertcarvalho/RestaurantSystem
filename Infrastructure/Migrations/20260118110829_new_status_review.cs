using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_status_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "name" },
                values: new object[] { new DateTime(2026, 1, 18, 11, 8, 28, 861, DateTimeKind.Utc).AddTicks(2928), "REVIEW" });

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "name" },
                values: new object[] { new DateTime(2026, 1, 18, 11, 8, 28, 861, DateTimeKind.Utc).AddTicks(2929), "COMPLETED" });

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "created_at", "name" },
                values: new object[] { new DateTime(2026, 1, 18, 11, 8, 28, 861, DateTimeKind.Utc).AddTicks(2930), "CANCELLED" });

            migrationBuilder.InsertData(
                table: "reservation_status",
                columns: new[] { "id", "created_at", "name" },
                values: new object[] { 7, new DateTime(2026, 1, 18, 11, 8, 28, 861, DateTimeKind.Utc).AddTicks(2931), "NO SHOW" });

            migrationBuilder.CreateIndex(
                name: "ix_reservation_guid",
                table: "reservation",
                column: "guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_token",
                table: "refresh_token",
                column: "token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_reservation_guid",
                table: "reservation");

            migrationBuilder.DropIndex(
                name: "ix_refresh_token_token",
                table: "refresh_token");

            migrationBuilder.DeleteData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "name" },
                values: new object[] { new DateTime(2026, 1, 17, 23, 49, 59, 54, DateTimeKind.Utc).AddTicks(2727), "COMPLETED" });

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "name" },
                values: new object[] { new DateTime(2026, 1, 17, 23, 49, 59, 54, DateTimeKind.Utc).AddTicks(2728), "CANCELLED" });

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "created_at", "name" },
                values: new object[] { new DateTime(2026, 1, 17, 23, 49, 59, 54, DateTimeKind.Utc).AddTicks(2729), "NO SHOW" });
        }
    }
}
