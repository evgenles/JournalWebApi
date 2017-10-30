using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DJournalWebApi.Migrations
{
    public partial class ChangedDeleteAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_SheetDates_SheetDatesId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Sheets_SheetId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_SheetStudents_SheetStudentId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupSheets_Sheets_SheetId",
                table: "GroupSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_SheetStudents_Students_StudentId",
                table: "SheetStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_SheetDates_SheetDatesId",
                table: "Cells",
                column: "SheetDatesId",
                principalTable: "SheetDates",
                principalColumn: "SheetDatesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Sheets_SheetId",
                table: "Cells",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "SheetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_SheetStudents_SheetStudentId",
                table: "Cells",
                column: "SheetStudentId",
                principalTable: "SheetStudents",
                principalColumn: "SheetStudentsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSheets_Sheets_SheetId",
                table: "GroupSheets",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "SheetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheetStudents_Students_StudentId",
                table: "SheetStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_SheetDates_SheetDatesId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Sheets_SheetId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Cells_SheetStudents_SheetStudentId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupSheets_Sheets_SheetId",
                table: "GroupSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_SheetStudents_Students_StudentId",
                table: "SheetStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_SheetDates_SheetDatesId",
                table: "Cells",
                column: "SheetDatesId",
                principalTable: "SheetDates",
                principalColumn: "SheetDatesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Sheets_SheetId",
                table: "Cells",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "SheetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_SheetStudents_SheetStudentId",
                table: "Cells",
                column: "SheetStudentId",
                principalTable: "SheetStudents",
                principalColumn: "SheetStudentsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSheets_Sheets_SheetId",
                table: "GroupSheets",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "SheetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SheetStudents_Students_StudentId",
                table: "SheetStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
