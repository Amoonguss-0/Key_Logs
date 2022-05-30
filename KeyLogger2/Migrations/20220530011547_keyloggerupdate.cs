using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyLogger2.Migrations
{
    public partial class keyloggerupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Password_Member_MembersMemberId",
                table: "Password");

            migrationBuilder.RenameColumn(
                name: "MembersMemberId",
                table: "Password",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Password_MembersMemberId",
                table: "Password",
                newName: "IX_Password_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Password_Member_MemberId",
                table: "Password",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Password_Member_MemberId",
                table: "Password");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Password",
                newName: "MembersMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Password_MemberId",
                table: "Password",
                newName: "IX_Password_MembersMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Password_Member_MembersMemberId",
                table: "Password",
                column: "MembersMemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
