using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.DAL.Migrations
{
    public partial class NewTablePassengerOfShareRides : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerOfShareRide_ShareRides_PassengerShareRidesId",
                table: "PassengerOfShareRide");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerOfShareRide_Users_PassengersId",
                table: "PassengerOfShareRide");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareRides_Cars_CarId",
                table: "ShareRides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerOfShareRide",
                table: "PassengerOfShareRide");

            migrationBuilder.DropIndex(
                name: "IX_PassengerOfShareRide_PassengersId",
                table: "PassengerOfShareRide");

            migrationBuilder.RenameColumn(
                name: "PassengersId",
                table: "PassengerOfShareRide",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PassengerShareRidesId",
                table: "PassengerOfShareRide",
                newName: "ShareRideId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "ShareRides",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PassengerId",
                table: "PassengerOfShareRide",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerOfShareRide",
                table: "PassengerOfShareRide",
                columns: new[] { "PassengerId", "ShareRideId" });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerOfShareRide_ShareRideId",
                table: "PassengerOfShareRide",
                column: "ShareRideId");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerOfShareRide_ShareRides_ShareRideId",
                table: "PassengerOfShareRide",
                column: "ShareRideId",
                principalTable: "ShareRides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerOfShareRide_Users_PassengerId",
                table: "PassengerOfShareRide",
                column: "PassengerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShareRides_Cars_CarId",
                table: "ShareRides",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerOfShareRide_ShareRides_ShareRideId",
                table: "PassengerOfShareRide");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerOfShareRide_Users_PassengerId",
                table: "PassengerOfShareRide");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareRides_Cars_CarId",
                table: "ShareRides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerOfShareRide",
                table: "PassengerOfShareRide");

            migrationBuilder.DropIndex(
                name: "IX_PassengerOfShareRide_ShareRideId",
                table: "PassengerOfShareRide");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "PassengerOfShareRide");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PassengerOfShareRide",
                newName: "PassengersId");

            migrationBuilder.RenameColumn(
                name: "ShareRideId",
                table: "PassengerOfShareRide",
                newName: "PassengerShareRidesId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "ShareRides",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerOfShareRide",
                table: "PassengerOfShareRide",
                columns: new[] { "PassengerShareRidesId", "PassengersId" });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerOfShareRide_PassengersId",
                table: "PassengerOfShareRide",
                column: "PassengersId");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerOfShareRide_ShareRides_PassengerShareRidesId",
                table: "PassengerOfShareRide",
                column: "PassengerShareRidesId",
                principalTable: "ShareRides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerOfShareRide_Users_PassengersId",
                table: "PassengerOfShareRide",
                column: "PassengersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShareRides_Cars_CarId",
                table: "ShareRides",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
