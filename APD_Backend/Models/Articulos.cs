using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Articulos")]
    public class Articulos {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public string nombre {get; set;}
        public int stock {get; set;}
        public float precio {get; set;}
        public int idProveedor {get; set;}
        [ForeignKey("idProveedor")]
        public Proveedores proveedores {get; set;}

    }
}