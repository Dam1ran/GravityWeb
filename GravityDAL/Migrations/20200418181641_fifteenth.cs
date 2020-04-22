using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class fifteenth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserCoaches",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachId = table.Column<long>(nullable: false),
                    ApplicationUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCoaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserCoaches_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserCoaches_Users_CoachId",
                        column: x => x.CoachId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCoaches_ApplicationUserId",
                table: "AppUserCoaches",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCoaches_CoachId_ApplicationUserId",
                table: "AppUserCoaches",
                columns: new[] { "CoachId", "ApplicationUserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCoaches");
        }
    }
}
