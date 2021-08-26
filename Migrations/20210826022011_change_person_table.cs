using Microsoft.EntityFrameworkCore.Migrations;

namespace netcore.Migrations
{
    public partial class change_person_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "tb_m_persons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_persons",
                table: "tb_m_persons",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_persons",
                table: "tb_m_persons");

            migrationBuilder.RenameTable(
                name: "tb_m_persons",
                newName: "Persons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "NIK");
        }
    }
}
