using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DJournalWebApi.Migrations
{
    public partial class FixedGroupSheetFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSheets_Groups_GroupId",
                table: "GroupSheets");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSheets_Groups_GroupId",
                table: "GroupSheets",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSheets_Groups_GroupId",
                table: "GroupSheets");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSheets_Groups_GroupId",
                table: "GroupSheets",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
