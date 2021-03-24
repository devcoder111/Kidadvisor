using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KidAdvisor.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordCreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastEditorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Appartement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordCreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastEditorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessId);
                    table.ForeignKey(
                        name: "FK_Businesses_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "FirstName", "LastEditDate", "LastEditorId", "LastName", "RecordCreatorId" },
                values: new object[] { new Guid("9f1c7846-19c5-42e7-b2c6-265934a42214"), new DateTime(2021, 2, 3, 5, 38, 11, 867, DateTimeKind.Local).AddTicks(619), "Mike", new DateTime(2021, 2, 3, 5, 38, 11, 868, DateTimeKind.Local).AddTicks(8793), new Guid("1939f2b1-6eb4-4c46-87a9-9e7de111a789"), "Tyson", new Guid("1939f2b1-6eb4-4c46-87a9-9e7de111a789") });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "BusinessId", "Appartement", "City", "Country", "DateCreated", "Description", "LastEditDate", "LastEditorId", "Name", "OwnerId", "PostalCode", "Province", "RecordCreatorId", "StreetAddress" },
                values: new object[] { new Guid("3fded86a-2f50-4c7d-8403-81f58bfed969"), "15F", "NewYork", "USA", new DateTime(2021, 2, 3, 5, 38, 11, 903, DateTimeKind.Local).AddTicks(4331), null, new DateTime(2021, 2, 3, 5, 38, 11, 903, DateTimeKind.Local).AddTicks(4389), new Guid("00000000-0000-0000-0000-000000000000"), "Pizza Hut", new Guid("9f1c7846-19c5-42e7-b2c6-265934a42214"), "5000", null, new Guid("00000000-0000-0000-0000-000000000000"), "6135 Hyatt Trail Suit" });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
