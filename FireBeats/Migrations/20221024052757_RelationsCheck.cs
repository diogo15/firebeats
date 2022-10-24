using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireBeats.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Users",
                newName: "CitiesId");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Cities",
                newName: "CountriesId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                newName: "IX_Cities_CountriesId");

            migrationBuilder.AddColumn<bool>(
                name: "Artist",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Songs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PlaylistsId",
                table: "Songs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaylistCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CitiesId",
                table: "Users",
                column: "CitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_PlaylistsId",
                table: "Songs",
                column: "PlaylistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountriesId",
                table: "Cities",
                column: "CountriesId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Playlists_PlaylistsId",
                table: "Songs",
                column: "PlaylistsId",
                principalTable: "Playlists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CitiesId",
                table: "Users",
                column: "CitiesId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountriesId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Playlists_PlaylistsId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CitiesId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Users_CitiesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Songs_PlaylistsId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "PlaylistsId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "CitiesId",
                table: "Users",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "CountriesId",
                table: "Cities",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountriesId",
                table: "Cities",
                newName: "IX_Cities_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
