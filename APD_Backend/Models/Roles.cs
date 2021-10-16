using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Roles")]
    public class Roles {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public string nombre {get; set;}
    }
}