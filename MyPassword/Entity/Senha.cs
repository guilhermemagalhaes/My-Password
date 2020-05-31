using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Cache;
using System.Text;

namespace MyPassword.Entity
{
    public class Senha
    {
        [Key]
        public int SenhaId { get; set; }
        [ForeignKey("Plataforma")]
        public int PlataformaId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Usuario { get; set; }
        [Required]
        [MaxLength(255)]
        public string SenhaDesc { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("GETDATE()")]
        [DisplayName("Data Cadastro")]
        public DateTime DataCadastro { get; set; }
        public Plataforma Plataforma { get; set; }

    }
}
