using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTeeth.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuditColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Dentists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Dentists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Dentists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Dentists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DentalOffices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "DentalOffices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "DentalOffices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "DentalOffices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Dentists");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Dentists");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Dentists");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Dentists");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DentalOffices");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "DentalOffices");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "DentalOffices");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "DentalOffices");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Appointments");
        }
    }
}
