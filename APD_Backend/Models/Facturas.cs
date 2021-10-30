using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Facturas")]
    public class Facturas {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public int idCliente {get; set;}
        [ForeignKey("idCliente")]
        public Clientes clientes {get; set;}
        public int idPedido {get; set;}
        [ForeignKey("idPedido")]
        public Pedidos pedidos {get; set;}
        public int idFormaDePago {get; set;}
        [ForeignKey("idFormaDePago")]
        public FormasDePago formasdepago {get; set;}
    }
}