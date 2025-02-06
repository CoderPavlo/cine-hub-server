using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cine_hub_server.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionAudTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Auditoriums_AuditoriumId",
                schema: "app",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_AuditoriumId",
                schema: "app",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "AuditoriumId",
                schema: "app",
                table: "Sessions",
                newName: "FilmName");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                schema: "app",
                table: "Sessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                schema: "app",
                table: "Sessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "SessionAuditoriums",
                schema: "app",
                columns: table => new
                {
                    SessionId = table.Column<string>(type: "text", nullable: false),
                    AuditoriumId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAuditoriums", x => new { x.SessionId, x.AuditoriumId });
                    table.ForeignKey(
                        name: "FK_SessionAuditoriums_Auditoriums_AuditoriumId",
                        column: x => x.AuditoriumId,
                        principalSchema: "app",
                        principalTable: "Auditoriums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionAuditoriums_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "app",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionAuditoriums_AuditoriumId",
                schema: "app",
                table: "SessionAuditoriums",
                column: "AuditoriumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionAuditoriums",
                schema: "app");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "app",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "app",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "FilmName",
                schema: "app",
                table: "Sessions",
                newName: "AuditoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AuditoriumId",
                schema: "app",
                table: "Sessions",
                column: "AuditoriumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Auditoriums_AuditoriumId",
                schema: "app",
                table: "Sessions",
                column: "AuditoriumId",
                principalSchema: "app",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
