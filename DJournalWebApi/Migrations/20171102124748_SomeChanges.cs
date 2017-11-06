using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DJournalWebApi.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Sheets_SheetId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Subjects_SubjectId",
                table: "Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Cells_SheetId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "SheetId",
                table: "Cells");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Semestr",
                table: "Sheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubjectType",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Sheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Subjects_SubjectId",
                table: "Sheets",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Subjects_SubjectId",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Semestr",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "SubjectType",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Sheets");

            migrationBuilder.AddColumn<Guid>(
                name: "SheetId",
                table: "Cells",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cells_SheetId",
                table: "Cells",
                column: "SheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Sheets_SheetId",
                table: "Cells",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "SheetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Subjects_SubjectId",
                table: "Sheets",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
