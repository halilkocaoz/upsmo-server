using Microsoft.EntityFrameworkCore.Migrations;

namespace UpsMo.Data.Migrations
{
    public partial class dropForeignKEy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Monitors_MonitorID",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_MonitorID",
                table: "Responses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Responses_MonitorID",
                table: "Responses",
                column: "MonitorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Monitors_MonitorID",
                table: "Responses",
                column: "MonitorID",
                principalTable: "Monitors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
