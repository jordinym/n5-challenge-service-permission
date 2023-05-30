using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N5Permission.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permission_type",
                columns: table => new
                {
                    in_id = table.Column<int>(type: "int", nullable: false, comment: "Unique ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vc_description = table.Column<string>(type: "varchar(120)", nullable: false, comment: "Permission description"),
                    vc_creation_user = table.Column<string>(type: "varchar(120)", nullable: false, comment: "Creation user"),
                    dt_creation_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Creation date"),
                    vc_modification_user = table.Column<string>(type: "varchar(120)", nullable: true, comment: "Modification user"),
                    dt_modification_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Modification date"),
                    bt_status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission_type", x => x.in_id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    in_id = table.Column<int>(type: "int", nullable: false, comment: "Unique ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vc_employee_forename = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Employee Forename"),
                    vc_employee_surname = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Employee Surname"),
                    in_permission_type_id = table.Column<int>(type: "int", nullable: false, comment: "Permission Type"),
                    vc_creation_user = table.Column<string>(type: "varchar(120)", nullable: false, comment: "Creation user"),
                    dt_creation_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Creation date"),
                    vc_modification_user = table.Column<string>(type: "varchar(120)", nullable: true, comment: "Modification user"),
                    dt_modification_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Modification date"),
                    bt_status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.in_id);
                    table.ForeignKey(
                        name: "FK_permission_permission_type_in_permission_type_id",
                        column: x => x.in_permission_type_id,
                        principalTable: "permission_type",
                        principalColumn: "in_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_permission_in_permission_type_id",
                table: "permission",
                column: "in_permission_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "permission_type");
        }
    }
}
