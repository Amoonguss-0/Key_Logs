using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyLogger2.Migrations
{
    public partial class updateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Password",
                newName: "MembersMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Password_MembersMemberId",
                table: "Password",
                column: "MembersMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Password_Member_MembersMemberId",
                table: "Password",
                column: "MembersMemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Password_Member_MembersMemberId",
                table: "Password");

            migrationBuilder.DropIndex(
                name: "IX_Password_MembersMemberId",
                table: "Password");

            migrationBuilder.RenameColumn(
                name: "MembersMemberId",
                table: "Password",
                newName: "MemberId");
        }
    }
}
