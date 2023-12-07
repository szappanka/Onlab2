using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<int>(type: "int", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "LastUpdate", "IsActivated", "Coordinates" },
                values: new object[,]
                {
                    { 1, "Harry Potter", 0, true, "0, 0, 0" },
                    { 2, "Hermione Granger", 0, true, "0, 0, 0" },
                    { 3, "Ron Weasley", 0, true, "0, 0, 0" },
                    { 4, "Albus Dumbledore", 0, true, "0, 0, 0" }
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EscapeRoomCourseTasks_TaskId",
            //    table: "EscapeRoomCourseTasks",
            //    column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
