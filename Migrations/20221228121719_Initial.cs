using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pedidosdap5.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    idestado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.idestado);
                });

            migrationBuilder.CreateTable(
                name: "Grados",
                columns: table => new
                {
                    idGrado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grado = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grados", x => x.idGrado);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    idtecnico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tecnico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cuentarina = table.Column<string>(name: "cuenta_rina", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    interno = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tecnicos", x => x.idtecnico);
                });

            migrationBuilder.CreateTable(
                name: "ViewModelPedido",
                columns: table => new
                {
                    Idpedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcionpedido = table.Column<string>(name: "Descripcion_pedido", type: "nvarchar(max)", nullable: false),
                    Usuario1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tecnico1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcionestado = table.Column<string>(name: "Descripcion_estado", type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewModelPedido", x => x.Idpedido);
                });

            migrationBuilder.CreateTable(
                name: "ViewModelSoluciones",
                columns: table => new
                {
                    IdSolucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    Pedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TecnicoDescricpcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSolucion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewModelSoluciones", x => x.IdSolucion);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idusuaio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    grado = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Interno = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    telparticular = table.Column<string>(name: "tel_particular", type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.idusuaio);
                    table.ForeignKey(
                        name: "FK_Usuarios_Grados",
                        column: x => x.grado,
                        principalTable: "Grados",
                        principalColumn: "idGrado");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    idpedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario = table.Column<int>(type: "int", nullable: false),
                    tecnico = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<int>(type: "int", nullable: true),
                    fechainicio = table.Column<DateTime>(name: "fecha_inicio", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.idpedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Estados",
                        column: x => x.estado,
                        principalTable: "Estados",
                        principalColumn: "idestado");
                    table.ForeignKey(
                        name: "FK_pedidos_tecnicos",
                        column: x => x.tecnico,
                        principalTable: "Tecnicos",
                        principalColumn: "idtecnico");
                    table.ForeignKey(
                        name: "FK_pedidos_usuarios",
                        column: x => x.usuario,
                        principalTable: "Usuarios",
                        principalColumn: "idusuaio");
                });

            migrationBuilder.CreateTable(
                name: "Soluciones",
                columns: table => new
                {
                    idsolucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idpedido = table.Column<int>(type: "int", nullable: false),
                    solucion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    fechasolucion = table.Column<DateTime>(name: "fecha_solucion", type: "datetime", nullable: true),
                    tecnico = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_soluciones", x => x.idsolucion);
                    table.ForeignKey(
                        name: "FK_Soluciones_Tecnico",
                        column: x => x.tecnico,
                        principalTable: "Tecnicos",
                        principalColumn: "idtecnico");
                    table.ForeignKey(
                        name: "FK_soluciones_pedidos",
                        column: x => x.idpedido,
                        principalTable: "Pedidos",
                        principalColumn: "idpedido");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_estado",
                table: "Pedidos",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_tecnico",
                table: "Pedidos",
                column: "tecnico");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_usuario",
                table: "Pedidos",
                column: "usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Soluciones_idpedido",
                table: "Soluciones",
                column: "idpedido");

            migrationBuilder.CreateIndex(
                name: "IX_Soluciones_tecnico",
                table: "Soluciones",
                column: "tecnico");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_grado",
                table: "Usuarios",
                column: "grado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Soluciones");

            migrationBuilder.DropTable(
                name: "ViewModelPedido");

            migrationBuilder.DropTable(
                name: "ViewModelSoluciones");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Grados");
        }
    }
}
