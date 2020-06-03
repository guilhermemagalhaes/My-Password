using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPassword.Entity
{
    public class Plataforma
    {
        [Key]
        public int PlataformaId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatória")]
        [MaxLength(255)]
        public string URL { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [DisplayName("Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        public ICollection<Senha> Senhas { get; set; }
    }
}
