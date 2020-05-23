using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class renamedWoRoutine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WoRoutines_WoRoutineId",
                table: "Workout");

            migrationBuilder.DropTable(
                name: "WoRoutines");

            migrationBuilder.DropIndex(
                name: "IX_Workout_WoRoutineId",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "WoRoutineId",
                table: "Workout");

            migrationBuilder.AddColumn<long>(
                name: "RoutineId",
                table: "Workout",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Routines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workout_RoutineId",
                table: "Workout",
                column: "RoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Routines_RoutineId",
                table: "Workout",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Routines_RoutineId",
                table: "Workout");

            migrationBuilder.DropTable(
                name: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Workout_RoutineId",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "RoutineId",
                table: "Workout");

            migrationBuilder.AddColumn<long>(
                name: "WoRoutineId",
                table: "Workout",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "WoRoutines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoRoutines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workout_WoRoutineId",
                table: "Workout",
                column: "WoRoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WoRoutines_WoRoutineId",
                table: "Workout",
                column: "WoRoutineId",
                principalTable: "WoRoutines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
