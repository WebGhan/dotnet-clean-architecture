using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTeeth.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DentistAssignmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DentistAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DentistId = table.Column<Guid>(type: "uuid", nullable: false),
                    DentalOfficeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentistAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DentistAssignments_DentalOffices_DentalOfficeId",
                        column: x => x.DentalOfficeId,
                        principalTable: "DentalOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DentistAssignments_Dentists_DentistId",
                        column: x => x.DentistId,
                        principalTable: "Dentists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DentistAssignments_DentalOfficeId",
                table: "DentistAssignments",
                column: "DentalOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DentistAssignments_DentistId_DentalOfficeId",
                table: "DentistAssignments",
                columns: new[] { "DentistId", "DentalOfficeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DentistAssignments");
        }
    }
}
