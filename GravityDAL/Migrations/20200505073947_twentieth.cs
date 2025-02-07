﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GravityDAL.Migrations
{
    public partial class twentieth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ExerciseSet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ExerciseSet");
        }
    }
}
