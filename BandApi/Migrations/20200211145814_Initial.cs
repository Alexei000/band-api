using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BandApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Founded = table.Column<DateTime>(nullable: false),
                    MainGenre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    BandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "Founded", "MainGenre", "Name" },
                values: new object[,]
                {
                    { new Guid("423d5dc6-64ba-4556-9032-88c9ad246eba"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heavy Metal", "Metallica" },
                    { new Guid("c75c4a5c-890e-491e-85de-933ea2ab25ab"), new DateTime(1985, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rock", "Guns N Roses" },
                    { new Guid("806b7c4e-242b-456d-bcf2-e50b9be83393"), new DateTime(1965, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Disco", "ABBA" },
                    { new Guid("6de68de3-0e30-4756-8070-6747506631f6"), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alternative", "Oasis" },
                    { new Guid("0673514e-80ec-4a4a-9b86-fd067efe9217"), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alternative", "Oasis" },
                    { new Guid("2bed73ff-2990-4302-90d2-b0be7d45db4e"), new DateTime(1981, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pop", "A-ha" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "BandId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("b4425cfd-b6a1-4708-a521-17060d6d0d7d"), new Guid("423d5dc6-64ba-4556-9032-88c9ad246eba"), "Best heavy metal albums", "Master of Puppets" },
                    { new Guid("004c1ca2-aa10-442c-970d-20ef26542994"), new Guid("c75c4a5c-890e-491e-85de-933ea2ab25ab"), "Amazing Rock Album", "Appetite for Destruction" },
                    { new Guid("f29e9274-1e56-48d0-93a9-2c1f1a95eb0a"), new Guid("806b7c4e-242b-456d-bcf2-e50b9be83393"), "Very Groovy Album", "Waterloo" },
                    { new Guid("de00294c-54dc-41c4-b5f7-ccc886e7b0b2"), new Guid("0673514e-80ec-4a4a-9b86-fd067efe9217"), "Oasis best", "Be Here Now" },
                    { new Guid("119e5ac0-6bcb-4e0c-9daa-0ac10c2ea4c6"), new Guid("2bed73ff-2990-4302-90d2-b0be7d45db4e"), "Debut Album", "Hunting High and Low" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_BandId",
                table: "Albums",
                column: "BandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Bands");
        }
    }
}
