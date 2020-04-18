using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieSessions.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Draft = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    MinutesRuntime = table.Column<int>(nullable: false),
                    Synopsis = table.Column<string>(nullable: false),
                    Director = table.Column<string>(nullable: false),
                    Actors = table.Column<string>(nullable: false),
                    Poster = table.Column<string>(nullable: false),
                    YouTubeEmbed = table.Column<string>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
