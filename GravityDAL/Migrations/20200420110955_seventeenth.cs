using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class seventeenth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrimaryMuscleId",
                table: "ExerciseTemplates",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SecondaryMuscleId",
                table: "ExerciseTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplates_PrimaryMuscleId",
                table: "ExerciseTemplates",
                column: "PrimaryMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplates_SecondaryMuscleId",
                table: "ExerciseTemplates",
                column: "SecondaryMuscleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTemplates_Muscles_PrimaryMuscleId",
                table: "ExerciseTemplates",
                column: "PrimaryMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTemplates_Muscles_SecondaryMuscleId",
                table: "ExerciseTemplates",
                column: "SecondaryMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTemplates_Muscles_PrimaryMuscleId",
                table: "ExerciseTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTemplates_Muscles_SecondaryMuscleId",
                table: "ExerciseTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseTemplates_PrimaryMuscleId",
                table: "ExerciseTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseTemplates_SecondaryMuscleId",
                table: "ExerciseTemplates");

            migrationBuilder.DropColumn(
                name: "PrimaryMuscleId",
                table: "ExerciseTemplates");

            migrationBuilder.DropColumn(
                name: "SecondaryMuscleId",
                table: "ExerciseTemplates");
        }
    }
}
