using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO CATEGORIAS(NOME, IMAGEMURL) VALUES ('Bebidas', 'bebidas.jpg')");
            mb.Sql("INSERT INTO CATEGORIAS(NOME, IMAGEMURL) VALUES ('Lanches', 'lanches.jpg')");
            mb.Sql("INSERT INTO CATEGORIAS(NOME, IMAGEMURL) VALUES ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
