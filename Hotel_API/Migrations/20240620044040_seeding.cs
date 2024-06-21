using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_API.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guest_GuestId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Room_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Guest_GuestId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotels_HotelId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomTypes_RoomTypeId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameIndex(
                name: "IX_Room_RoomTypeId",
                table: "Rooms",
                newName: "IX_Rooms_RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_HotelId",
                table: "Rooms",
                newName: "IX_Rooms_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CheckOutAt",
                value: new DateTime(2024, 6, 20, 7, 40, 40, 124, DateTimeKind.Local).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartedDate",
                value: new DateTime(2024, 6, 20, 7, 40, 40, 124, DateTimeKind.Local).AddTicks(3154));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 20, 7, 40, 40, 124, DateTimeKind.Local).AddTicks(3455));

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Guests_GuestId",
                table: "Payments",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Guests_GuestId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Room",
                newName: "IX_Room_RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_HotelId",
                table: "Room",
                newName: "IX_Room_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CheckOutAt",
                value: new DateTime(2024, 6, 18, 2, 40, 57, 192, DateTimeKind.Local).AddTicks(5593));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartedDate",
                value: new DateTime(2024, 6, 18, 2, 40, 57, 192, DateTimeKind.Local).AddTicks(5248));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 18, 2, 40, 57, 192, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guest_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Room_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Guest_GuestId",
                table: "Payments",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotels_HotelId",
                table: "Room",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomTypes_RoomTypeId",
                table: "Room",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
