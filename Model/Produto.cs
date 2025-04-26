using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Demo_EFC.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }

}
