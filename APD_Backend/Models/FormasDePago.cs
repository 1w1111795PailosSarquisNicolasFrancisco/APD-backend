using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("FormasDePago")]
    public class FormasDePago {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id {get; set;}
        public string formaDePago {get; set;}
    }
}