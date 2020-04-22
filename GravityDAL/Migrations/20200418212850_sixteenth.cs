using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class sixteenth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalClients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalClients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalClients_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalClients_ApplicationUserId",
                table: "PersonalClients",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalClients_Email",
                table: "PersonalClients",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalClients_Email_ApplicationUserId",
                table: "PersonalClients",
                columns: new[] { "Email", "ApplicationUserId" },
                unique: true);
        }
    }
}
