using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addingmoreproductinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "OrderProduct",
                newName: "ProductTitle");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "CartItem",
                newName: "ProductTitle");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderProduct",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductCategory",
                table: "OrderProduct",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "OrderProduct",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "OrderProduct",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductCategory",
                table: "CartItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "CartItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CartItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "CartItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                columns: new[] { "OrderId", "ProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "ProductTitle",
                table: "OrderProduct",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "ProductTitle",
                table: "CartItem",
                newName: "ProductName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                columns: new[] { "OrderId", "ProductName" });
        }
    }
}
