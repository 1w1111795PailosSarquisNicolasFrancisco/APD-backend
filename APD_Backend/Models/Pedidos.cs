using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Pedidos")]
    public class Pedidos {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public string estado {get; set;}
        public int idCliente {get; set;}
        [ForeignKey("idCliente")]
        public Clientes Clientes {get; set;}
    }
}