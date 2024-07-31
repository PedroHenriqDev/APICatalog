using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations;

/// <inheritdoc />
public partial class SecondMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            table: "Products",
            type: "numeric(10,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Products",
            type: "character varying(80)",
            maxLength: 80,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ImageUrl",
            table: "Products",
            type: "character varying(300)",
            maxLength: 300,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Products",
            type: "character varying(300)",
            maxLength: 300,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Categories",
            type: "character varying(80)",
            maxLength: 80,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ImageUrl",
            table: "Categories",
            type: "character varying(300)",
            maxLength: 300,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            table: "Products",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Products",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(80)",
            oldMaxLength: 80);

        migrationBuilder.AlterColumn<string>(
            name: "ImageUrl",
            table: "Products",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(300)",
            oldMaxLength: 300);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Products",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(300)",
            oldMaxLength: 300);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Categories",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(80)",
            oldMaxLength: 80);

        migrationBuilder.AlterColumn<string>(
            name: "ImageUrl",
            table: "Categories",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(300)",
            oldMaxLength: 300);
    }
}
