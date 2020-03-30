using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PersonalClients_Email_ApplicationUserId",
                table: "PersonalClients",
                columns: new[] { "Email", "ApplicationUserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalClients_Email_ApplicationUserId",
                table: "PersonalClients");
        }
    }
}
