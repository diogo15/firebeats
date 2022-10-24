using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireBeats.Migrations
{
    public partial class PlaylistsSongsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Playlists_PlaylistsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PlaylistsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlaylistsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Favorite",
                table: "Songs",
                newName: "isFavorite");

            migrationBuilder.AddColumn<Guid>(
                name: "SongsId",
                table: "Playlists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "Playlists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_SongsId",
                table: "Playlists",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UsersId",
                table: "Playlists",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Songs_SongsId",
                table: "Playlists",
                column: "SongsId",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_UsersId",
                table: "Playlists",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Songs_SongsId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_UsersId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_SongsId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_UsersId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "SongsId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "isFavorite",
                table: "Songs",
                newName: "Favorite");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaylistsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlaylistsId",
                table: "Users",
                column: "PlaylistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Playlists_PlaylistsId",
                table: "Users",
                column: "PlaylistsId",
                principalTable: "Playlists",
                principalColumn: "Id");
        }
    }
}
