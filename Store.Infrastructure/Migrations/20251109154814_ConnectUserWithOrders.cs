using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConnectUserWithOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Orders_ProductId",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Products_ProductId1",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInformation_Orders_ProductId",
                table: "OrdersInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Collections_CollectionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProducts_ProductId",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderedProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrdersInformation",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersInformation_ProductId",
                table: "OrdersInformation",
                newName: "IX_OrdersInformation_OrderId");

            migrationBuilder.RenameColumn(
                name: "ProductId1",
                table: "OrderedProducts",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedProducts_ProductId1",
                table: "OrderedProducts",
                newName: "IX_OrderedProducts_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "OrderInformationId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "OrderedProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInformation_Orders_OrderId",
                table: "OrdersInformation",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Collections_CollectionId",
                table: "Products",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInformation_Orders_OrderId",
                table: "OrdersInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Collections_CollectionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderInformationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "OrderedProducts");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrdersInformation",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersInformation_OrderId",
                table: "OrdersInformation",
                newName: "IX_OrdersInformation_ProductId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderedProducts",
                newName: "ProductId1");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedProducts_OrderId",
                table: "OrderedProducts",
                newName: "IX_OrderedProducts_ProductId1");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderedProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_ProductId",
                table: "OrderedProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Orders_ProductId",
                table: "OrderedProducts",
                column: "ProductId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Products_ProductId1",
                table: "OrderedProducts",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInformation_Orders_ProductId",
                table: "OrdersInformation",
                column: "ProductId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Collections_CollectionId",
                table: "Products",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
