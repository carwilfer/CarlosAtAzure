using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Initialteste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmigoDoAmigo_Amigos_AmigoId",
                table: "AmigoDoAmigo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AmigoDoAmigo",
                table: "AmigoDoAmigo");

            migrationBuilder.RenameTable(
                name: "AmigoDoAmigo",
                newName: "AmigosDosAmigos");

            migrationBuilder.RenameIndex(
                name: "IX_AmigoDoAmigo_AmigoId",
                table: "AmigosDosAmigos",
                newName: "IX_AmigosDosAmigos_AmigoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmigosDosAmigos",
                table: "AmigosDosAmigos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmigosDosAmigos_Amigos_AmigoId",
                table: "AmigosDosAmigos",
                column: "AmigoId",
                principalTable: "Amigos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmigosDosAmigos_Amigos_AmigoId",
                table: "AmigosDosAmigos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AmigosDosAmigos",
                table: "AmigosDosAmigos");

            migrationBuilder.RenameTable(
                name: "AmigosDosAmigos",
                newName: "AmigoDoAmigo");

            migrationBuilder.RenameIndex(
                name: "IX_AmigosDosAmigos_AmigoId",
                table: "AmigoDoAmigo",
                newName: "IX_AmigoDoAmigo_AmigoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmigoDoAmigo",
                table: "AmigoDoAmigo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmigoDoAmigo_Amigos_AmigoId",
                table: "AmigoDoAmigo",
                column: "AmigoId",
                principalTable: "Amigos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
