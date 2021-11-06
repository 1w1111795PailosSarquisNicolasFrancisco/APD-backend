using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Clientes")]
    public class Clientes {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public string nombre {get; set;}
        public string cantidadPedidos {get; set;}
        public int clasificaciones {get; set;}
        public string telefono {get; set;}
        public string mail {get; set;}
        public string direccion {get; set;}
        public int idZona {get; set;}
        [ForeignKey("idZona")]
        public Zonas Zonas {get; set;}
    }
}