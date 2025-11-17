using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderFKAndOrderInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInformation_Orders_OrderId",
                table: "OrdersInformation");

            migrationBuilder.DropIndex(
                name: "IX_OrdersInformation_OrderId",
                table: "OrdersInformation");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrdersInformation");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderInformationId",
                table: "Orders",
                column: "OrderInformationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrdersInformation_OrderInformationId",
                table: "Orders",
                column: "OrderInformationId",
                principalTable: "OrdersInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrdersInformation_OrderInformationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderInformationId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrdersInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrdersInformation_OrderId",
                table: "OrdersInformation",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInformation_Orders_OrderId",
                table: "OrdersInformation",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
