using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingProject.Migrations
{
    public partial class SittingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 7, 29, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 7, 29, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 7, 29, 14, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 7, 29, 11, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 7, 29, 20, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 7, 29, 17, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 7, 30, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 7, 30, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 7, 30, 14, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 7, 30, 11, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 7, 30, 20, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 7, 30, 17, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 6, 24, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 6, 24, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 6, 24, 14, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 6, 24, 11, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 6, 24, 20, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 6, 24, 17, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 6, 25, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 6, 25, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 6, 25, 14, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 6, 25, 11, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 6, 25, 20, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 6, 25, 17, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
