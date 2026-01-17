using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reservation_add_column_transactionid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "transaction_id",
                table: "reservation",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 17, 2, 24, 502, DateTimeKind.Utc).AddTicks(8213));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 17, 2, 24, 502, DateTimeKind.Utc).AddTicks(8882));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 17, 2, 24, 502, DateTimeKind.Utc).AddTicks(8883));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 17, 2, 24, 502, DateTimeKind.Utc).AddTicks(8884));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 17, 2, 24, 502, DateTimeKind.Utc).AddTicks(8884));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 17, 2, 24, 502, DateTimeKind.Utc).AddTicks(8885));

            migrationBuilder.UpdateData(
                table: "restaurant",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 17, 2, 24, 503, DateTimeKind.Utc).AddTicks(8333));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transaction_id",
                table: "reservation");

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 16, 23, 31, 975, DateTimeKind.Utc).AddTicks(1518));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 16, 23, 31, 975, DateTimeKind.Utc).AddTicks(2188));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 16, 23, 31, 975, DateTimeKind.Utc).AddTicks(2189));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 16, 23, 31, 975, DateTimeKind.Utc).AddTicks(2190));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 16, 23, 31, 975, DateTimeKind.Utc).AddTicks(2191));

            migrationBuilder.UpdateData(
                table: "reservation_status",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 16, 23, 31, 975, DateTimeKind.Utc).AddTicks(2192));

            migrationBuilder.UpdateData(
                table: "restaurant",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 1, 17, 16, 23, 31, 976, DateTimeKind.Utc).AddTicks(2423));
        }
    }
}
