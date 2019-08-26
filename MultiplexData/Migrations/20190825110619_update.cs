using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiplexData.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatRun_AspNetUsers_UserId",
                table: "SeatRun");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SeatRun_RunId_SeatRoomId_UserId",
                table: "SeatRun");

            migrationBuilder.DropIndex(
                name: "IX_SeatRun_UserId",
                table: "SeatRun");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SeatRun");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SeatRun_RunId_SeatRoomId",
                table: "SeatRun",
                columns: new[] { "RunId", "SeatRoomId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SeatRun_RunId_SeatRoomId",
                table: "SeatRun");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SeatRun",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SeatRun_RunId_SeatRoomId_UserId",
                table: "SeatRun",
                columns: new[] { "RunId", "SeatRoomId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_SeatRun_UserId",
                table: "SeatRun",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatRun_AspNetUsers_UserId",
                table: "SeatRun",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
