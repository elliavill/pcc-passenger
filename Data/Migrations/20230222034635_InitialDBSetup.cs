using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.VisualBasic;

namespace Passenger.Data.Migrations
{
    public partial class InitialDBSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_email_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
