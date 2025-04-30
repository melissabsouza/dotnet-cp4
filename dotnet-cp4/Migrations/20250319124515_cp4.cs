using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_cp4.Migrations
{
    /// <inheritdoc />
    public partial class cp4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CP4_EVENT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TicketPrice = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    Local = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP4_EVENT", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CP4_EVENT");
        }
    }
}
