namespace MyGameScore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jogo")]
    public partial class Jogo
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime data_jogo { get; set; }

        public int pontuacao { get; set; }
    }
}
