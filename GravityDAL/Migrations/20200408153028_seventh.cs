using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class seventh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MuscleExercises_MuscleId",
                table: "MuscleExercises");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_MuscleId_ExerciseTemplateId",
                table: "MuscleExercises",
                columns: new[] { "MuscleId", "ExerciseTemplateId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MuscleExercises_MuscleId_ExerciseTemplateId",
                table: "MuscleExercises");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_MuscleId",
                table: "MuscleExercises",
                column: "MuscleId");
        }
    }
}
