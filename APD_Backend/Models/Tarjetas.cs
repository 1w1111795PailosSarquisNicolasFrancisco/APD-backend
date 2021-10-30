using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Tarjetas")]
    public class Tarjetas {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public int idCliente {get; set;}
        [ForeignKey("idCliente")]
        public Clientes clientes {get; set;}
        public int numero {get; set;}
        public DateTime fechaVencimiento {get; set;}

    }
}