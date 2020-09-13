using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PartiuAlmoco.Infra.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    FriendlyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RestaurantPollId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    WinnerRestaurantId = table.Column<Guid>(nullable: true),
                    TotalVotes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RestaurantPollId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantPolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    WinnerRestaurantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantPolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantPolls_Restaurants_WinnerRestaurantId",
                        column: x => x.WinnerRestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RestaurantPollId = table.Column<Guid>(nullable: true),
                    VoterId = table.Column<Guid>(nullable: true),
                    RestaurantId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_RestaurantPolls_RestaurantPollId",
                        column: x => x.RestaurantPollId,
                        principalTable: "RestaurantPolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Users_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollResults_RestaurantPollId",
                table: "PollResults",
                column: "RestaurantPollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollResults_WinnerRestaurantId",
                table: "PollResults",
                column: "WinnerRestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantPolls_WinnerRestaurantId",
                table: "RestaurantPolls",
                column: "WinnerRestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantPollId",
                table: "Restaurants",
                column: "RestaurantPollId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RestaurantId",
                table: "Votes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RestaurantPollId",
                table: "Votes",
                column: "RestaurantPollId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterId",
                table: "Votes",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollResults_RestaurantPolls_RestaurantPollId",
                table: "PollResults",
                column: "RestaurantPollId",
                principalTable: "RestaurantPolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PollResults_Restaurants_WinnerRestaurantId",
                table: "PollResults",
                column: "WinnerRestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantPolls_RestaurantPollId",
                table: "Restaurants",
                column: "RestaurantPollId",
                principalTable: "RestaurantPolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantPolls_RestaurantPollId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "PollResults");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RestaurantPolls");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
