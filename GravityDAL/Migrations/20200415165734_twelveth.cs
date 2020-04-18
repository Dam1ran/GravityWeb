using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class twelveth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WORoutines_WORoutineId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WORoutines",
                table: "WORoutines");

            migrationBuilder.RenameTable(
                name: "WORoutines",
                newName: "WoRoutines");

            migrationBuilder.RenameColumn(
                name: "WORoutineId",
                table: "Workout",
                newName: "WoRoutineId");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_WORoutineId",
                table: "Workout",
                newName: "IX_Workout_WoRoutineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WoRoutines",
                table: "WoRoutines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WoRoutines_WoRoutineId",
                table: "Workout",
                column: "WoRoutineId",
                principalTable: "WoRoutines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WoRoutines_WoRoutineId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WoRoutines",
                table: "WoRoutines");

            migrationBuilder.RenameTable(
                name: "WoRoutines",
                newName: "WORoutines");

            migrationBuilder.RenameColumn(
                name: "WoRoutineId",
                table: "Workout",
                newName: "WORoutineId");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_WoRoutineId",
                table: "Workout",
                newName: "IX_Workout_WORoutineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WORoutines",
                table: "WORoutines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WORoutines_WORoutineId",
                table: "Workout",
                column: "WORoutineId",
                principalTable: "WORoutines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
