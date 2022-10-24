using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireBeats.Migrations
{
    public partial class PlaylistRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Playlists_PlaylistsId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "PlaylistCount",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "PlaylistsId",
                table: "Songs",
                newName: "AlbumsId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_PlaylistsId",
                table: "Songs",
                newName: "IX_Songs_AlbumsId");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaylistsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlaylistsId",
                table: "Users",
                column: "PlaylistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumsId",
                table: "Songs",
                column: "AlbumsId",
                principalTable: "Albums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Playlists_PlaylistsId",
                table: "Users",
                column: "PlaylistsId",
                principalTable: "Playlists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumsId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Playlists_PlaylistsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Users_PlaylistsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlaylistsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AlbumsId",
                table: "Songs",
                newName: "PlaylistsId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_AlbumsId",
                table: "Songs",
                newName: "IX_Songs_PlaylistsId");

            migrationBuilder.AddColumn<int>(
                name: "PlaylistCount",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Playlists_PlaylistsId",
                table: "Songs",
                column: "PlaylistsId",
                principalTable: "Playlists",
                principalColumn: "Id");
        }
    }
}
