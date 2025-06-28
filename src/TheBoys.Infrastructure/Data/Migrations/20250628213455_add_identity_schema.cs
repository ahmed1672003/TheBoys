using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBoys.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_identity_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "identity");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "identity",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "identity",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "identity",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "identity",
                table: "Users",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "identity",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Users", schema: "identity");

            migrationBuilder.DropTable(name: "Roles", schema: "identity");
        }
    }
}
