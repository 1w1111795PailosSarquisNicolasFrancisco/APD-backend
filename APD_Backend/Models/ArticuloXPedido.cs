using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("ArticuloXPedido")]
    public class ArticuloXPedido {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public int idArticulo {get; set;}
        public int cantidad {get; set;}
        [ForeignKey("idArticulo")]
        public Articulos articulos {get; set;}
        public int idPedido {get; set;}
        [ForeignKey("idPedido")]
        public Pedidos pedidos {get; set;}
    }
}