using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "created_at", "email", "name", "phone" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(2396), "customer1@example.com", "Customer 1", "10000000001" },
                    { 2, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3522), "customer2@example.com", "Customer 2", "10000000002" },
                    { 3, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3530), "customer3@example.com", "Customer 3", "10000000003" },
                    { 4, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3557), "customer4@example.com", "Customer 4", "10000000004" },
                    { 5, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3559), "customer5@example.com", "Customer 5", "10000000005" },
                    { 6, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3566), "customer6@example.com", "Customer 6", "10000000006" },
                    { 7, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3568), "customer7@example.com", "Customer 7", "10000000007" },
                    { 8, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3570), "customer8@example.com", "Customer 8", "10000000008" },
                    { 9, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3571), "customer9@example.com", "Customer 9", "10000000009" },
                    { 10, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3574), "customer10@example.com", "Customer 10", "10000000010" },
                    { 11, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3576), "customer11@example.com", "Customer 11", "10000000011" },
                    { 12, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3577), "customer12@example.com", "Customer 12", "10000000012" },
                    { 13, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3579), "customer13@example.com", "Customer 13", "10000000013" },
                    { 14, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3584), "customer14@example.com", "Customer 14", "10000000014" },
                    { 15, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3586), "customer15@example.com", "Customer 15", "10000000015" },
                    { 16, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3588), "customer16@example.com", "Customer 16", "10000000016" },
                    { 17, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3589), "customer17@example.com", "Customer 17", "10000000017" },
                    { 18, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3592), "customer18@example.com", "Customer 18", "10000000018" },
                    { 19, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3594), "customer19@example.com", "Customer 19", "10000000019" },
                    { 20, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3595), "customer20@example.com", "Customer 20", "10000000020" },
                    { 21, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3597), "customer21@example.com", "Customer 21", "10000000021" },
                    { 22, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3598), "customer22@example.com", "Customer 22", "10000000022" },
                    { 23, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3600), "customer23@example.com", "Customer 23", "10000000023" },
                    { 24, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3601), "customer24@example.com", "Customer 24", "10000000024" },
                    { 25, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3603), "customer25@example.com", "Customer 25", "10000000025" },
                    { 26, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3604), "customer26@example.com", "Customer 26", "10000000026" },
                    { 27, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3606), "customer27@example.com", "Customer 27", "10000000027" },
                    { 28, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3608), "customer28@example.com", "Customer 28", "10000000028" },
                    { 29, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3609), "customer29@example.com", "Customer 29", "10000000029" },
                    { 30, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3616), "customer30@example.com", "Customer 30", "10000000030" },
                    { 31, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3618), "customer31@example.com", "Customer 31", "10000000031" },
                    { 32, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3619), "customer32@example.com", "Customer 32", "10000000032" },
                    { 33, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3621), "customer33@example.com", "Customer 33", "10000000033" },
                    { 34, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3664), "customer34@example.com", "Customer 34", "10000000034" },
                    { 35, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3666), "customer35@example.com", "Customer 35", "10000000035" },
                    { 36, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3668), "customer36@example.com", "Customer 36", "10000000036" },
                    { 37, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3669), "customer37@example.com", "Customer 37", "10000000037" },
                    { 38, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3671), "customer38@example.com", "Customer 38", "10000000038" },
                    { 39, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3672), "customer39@example.com", "Customer 39", "10000000039" },
                    { 40, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3674), "customer40@example.com", "Customer 40", "10000000040" },
                    { 41, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3676), "customer41@example.com", "Customer 41", "10000000041" },
                    { 42, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3677), "customer42@example.com", "Customer 42", "10000000042" },
                    { 43, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3679), "customer43@example.com", "Customer 43", "10000000043" },
                    { 44, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3680), "customer44@example.com", "Customer 44", "10000000044" },
                    { 45, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3682), "customer45@example.com", "Customer 45", "10000000045" },
                    { 46, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3683), "customer46@example.com", "Customer 46", "10000000046" },
                    { 47, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3685), "customer47@example.com", "Customer 47", "10000000047" },
                    { 48, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3687), "customer48@example.com", "Customer 48", "10000000048" },
                    { 49, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3688), "customer49@example.com", "Customer 49", "10000000049" },
                    { 50, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3690), "customer50@example.com", "Customer 50", "10000000050" },
                    { 51, new DateTime(2026, 1, 18, 12, 37, 7, 372, DateTimeKind.Utc).AddTicks(3691), "customer51@example.com", "Customer 51", "10000000051" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 51);
        }
    }
}
