using Microsoft.EntityFrameworkCore.Migrations;

namespace Passenger.Data.Migrations
{
    public partial class AddAuthentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_gender",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    trip_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trip_pickup_location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_fare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_distance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_start_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_end_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trip_availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.trip_id);
                    table.ForeignKey(
                        name: "FK_Trip_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_user_id",
                table: "Trip",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropColumn(
                name: "user_gender",
                table: "User");
        }
    }
}
