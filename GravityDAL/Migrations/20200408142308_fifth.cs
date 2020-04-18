using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "UsefulLinks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UsefulLinks",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Comments = table.Column<string>(maxLength: 200, nullable: true),
                    Tempo = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Muscles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MuscleExercise",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MuscleId = table.Column<long>(nullable: false),
                    ExerciseTemplateId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuscleExercise_ExerciseTemplates_ExerciseTemplateId",
                        column: x => x.ExerciseTemplateId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuscleExercise_Muscles_MuscleId",
                        column: x => x.MuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Calves" },
                    { 2L, "Quads" },
                    { 3L, "Hamstrings" },
                    { 4L, "Glutes" },
                    { 5L, "Abs" },
                    { 6L, "Core" },
                    { 7L, "Lower Back" },
                    { 8L, "Lats" },
                    { 9L, "Traps" },
                    { 10L, "Chest" },
                    { 11L, "Neck" },
                    { 12L, "Shoulders" },
                    { 13L, "Triceps" },
                    { 14L, "Biceps" },
                    { 15L, "Forearms" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercise_ExerciseTemplateId",
                table: "MuscleExercise",
                column: "ExerciseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercise_MuscleId",
                table: "MuscleExercise",
                column: "MuscleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuscleExercise");

            migrationBuilder.DropTable(
                name: "ExerciseTemplates");

            migrationBuilder.DropTable(
                name: "Muscles");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "UsefulLinks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UsefulLinks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
