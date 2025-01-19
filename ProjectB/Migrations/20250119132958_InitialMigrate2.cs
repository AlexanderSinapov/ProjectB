using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectB.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TextContent",
                table: "Lessons",
                type: "character varying(50000)",
                maxLength: 50000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50000)",
                oldMaxLength: 50000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentUrl",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TextContent",
                table: "Lessons",
                type: "character varying(50000)",
                maxLength: 50000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50000)",
                oldMaxLength: 50000);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentUrl",
                table: "Lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }
    }
}
