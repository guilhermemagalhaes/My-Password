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

        [DisplayName("Plataforma")]
        [ForeignKey("Plataforma")]
        public int PlataformaId { get; set; }

        [Required(ErrorMessage= "{0} é obrigatório")]
        [MaxLength(255)]
        [DisplayName("E-mail/Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(255)]
        [DisplayName("Senha")]
        public string SenhaDesc { get; set; }

        [DisplayName("Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        public Plataforma Plataforma { get; set; }
    }
}
