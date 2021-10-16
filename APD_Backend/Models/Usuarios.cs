using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Usuarios")]
    public class Usuarios {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public string usuario {get; set;}
        public string clave {get; set;}
        public string tokenSesion {get; set;}
        public int idRol {get; set;}
        [ForeignKey("idRol")]
        public Roles roles {get; set;}
    }
}