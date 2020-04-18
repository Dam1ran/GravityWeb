using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class sisxth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercise_ExerciseTemplates_ExerciseTemplateId",
                table: "MuscleExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercise_Muscles_MuscleId",
                table: "MuscleExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MuscleExercise",
                table: "MuscleExercise");

            migrationBuilder.RenameTable(
                name: "MuscleExercise",
                newName: "MuscleExercises");

            migrationBuilder.RenameIndex(
                name: "IX_MuscleExercise_MuscleId",
                table: "MuscleExercises",
                newName: "IX_MuscleExercises_MuscleId");

            migrationBuilder.RenameIndex(
                name: "IX_MuscleExercise_ExerciseTemplateId",
                table: "MuscleExercises",
                newName: "IX_MuscleExercises_ExerciseTemplateId");

            migrationBuilder.AlterColumn<string>(
                name: "Tempo",
                table: "ExerciseTemplates",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MuscleExercises",
                table: "MuscleExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercises_ExerciseTemplates_ExerciseTemplateId",
                table: "MuscleExercises",
                column: "ExerciseTemplateId",
                principalTable: "ExerciseTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercises_Muscles_MuscleId",
                table: "MuscleExercises",
                column: "MuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercises_ExerciseTemplates_ExerciseTemplateId",
                table: "MuscleExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercises_Muscles_MuscleId",
                table: "MuscleExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MuscleExercises",
                table: "MuscleExercises");

            migrationBuilder.RenameTable(
                name: "MuscleExercises",
                newName: "MuscleExercise");

            migrationBuilder.RenameIndex(
                name: "IX_MuscleExercises_MuscleId",
                table: "MuscleExercise",
                newName: "IX_MuscleExercise_MuscleId");

            migrationBuilder.RenameIndex(
                name: "IX_MuscleExercises_ExerciseTemplateId",
                table: "MuscleExercise",
                newName: "IX_MuscleExercise_ExerciseTemplateId");

            migrationBuilder.AlterColumn<string>(
                name: "Tempo",
                table: "ExerciseTemplates",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MuscleExercise",
                table: "MuscleExercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercise_ExerciseTemplates_ExerciseTemplateId",
                table: "MuscleExercise",
                column: "ExerciseTemplateId",
                principalTable: "ExerciseTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercise_Muscles_MuscleId",
                table: "MuscleExercise",
                column: "MuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
