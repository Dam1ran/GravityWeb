using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class addOurTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OurTeamMembers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarUrl = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 30, nullable: false),
                    Activity = table.Column<string>(maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Moto = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurTeamMembers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurTeamMembers");
        }
    }
}
