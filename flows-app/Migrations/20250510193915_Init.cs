using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace flows_app.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);   
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowSteps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlowId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StepId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowSteps_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowSteps_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowStepDependencies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlowStepId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DependsOnFlowStepId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowStepDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowStepDependencies_FlowSteps_DependsOnFlowStepId",
                        column: x => x.DependsOnFlowStepId,
                        principalTable: "FlowSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowStepDependencies_FlowSteps_FlowStepId",
                        column: x => x.FlowStepId,
                        principalTable: "FlowSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowStepFields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlowStepId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowStepFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowStepFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowStepFields_FlowSteps_FlowStepId",
                        column: x => x.FlowStepId,
                        principalTable: "FlowSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "F-0001", "Primer nombre" },
                    { "F-0002", "Segundo nombre" },
                    { "F-0003", "Primer apellido" },
                    { "F-0004", "Segundo apellido" },
                    { "F-0005", "Tipo de documento" },
                    { "F-0006", "Número de documento" }
                });

            migrationBuilder.InsertData(
                table: "Flows",
                columns: new[] { "Id", "Name" },
                values: new object[] { "FLW-0001", "Solicitud de producto financiero" });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "STP-0001", "Registro de usuario" },
                    { "STP-0002", "Formulario de datos personales" },
                    { "STP-0003", "Confirmación de correo" }
                });

            migrationBuilder.InsertData(
                table: "FlowSteps",
                columns: new[] { "Id", "FlowId", "Order", "StepId" },
                values: new object[,]
                {
                    { "FST-0001", "FLW-0001", 1, "STP-0001" },
                    { "FST-0002", "FLW-0001", 2, "STP-0002" },
                    { "FST-0003", "FLW-0001", 3, "STP-0003" }
                });

            migrationBuilder.InsertData(
                table: "FlowStepDependencies",
                columns: new[] { "Id", "DependsOnFlowStepId", "FlowStepId" },
                values: new object[,]
                {
                    { "1", "FST-0001", "FST-0002" },
                    { "2", "FST-0002", "FST-0003" }
                });

            migrationBuilder.InsertData(
                table: "FlowStepFields",
                columns: new[] { "Id", "Direction", "FieldId", "FlowStepId" },
                values: new object[,]
                {
                    { "FSF-0001", 1, "F-0005", "FST-0001" },
                    { "FSF-0002", 1, "F-0006", "FST-0001" },
                    { "FSF-0003", 1, "F-0001", "FST-0002" },
                    { "FSF-0004", 1, "F-0003", "FST-0002" },
                    { "FSF-0005", 2, "F-0001", "FST-0003" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowStepDependencies_DependsOnFlowStepId",
                table: "FlowStepDependencies",
                column: "DependsOnFlowStepId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStepDependencies_FlowStepId",
                table: "FlowStepDependencies",
                column: "FlowStepId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStepFields_FieldId",
                table: "FlowStepFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStepFields_FlowStepId",
                table: "FlowStepFields",
                column: "FlowStepId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSteps_FlowId",
                table: "FlowSteps",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSteps_StepId",
                table: "FlowSteps",
                column: "StepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowStepDependencies");

            migrationBuilder.DropTable(
                name: "FlowStepFields");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "FlowSteps");

            migrationBuilder.DropTable(
                name: "Flows");

            migrationBuilder.DropTable(
                name: "Steps");
        }
    }
}
