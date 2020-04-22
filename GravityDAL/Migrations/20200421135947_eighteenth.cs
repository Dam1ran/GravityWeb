using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class eighteenth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuscleExercises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MuscleExercises",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    MuscleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuscleExercises_ExerciseTemplates_ExerciseTemplateId",
                        column: x => x.ExerciseTemplateId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuscleExercises_Muscles_MuscleId",
                        column: x => x.MuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_ExerciseTemplateId",
                table: "MuscleExercises",
                column: "ExerciseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_MuscleId_ExerciseTemplateId",
                table: "MuscleExercises",
                columns: new[] { "MuscleId", "ExerciseTemplateId" },
                unique: true);
        }
    }
}
