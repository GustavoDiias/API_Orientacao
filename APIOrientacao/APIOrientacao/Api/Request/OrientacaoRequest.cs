using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class OrientacaoRequest
    {
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public int IdProjeto;
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public int IdPessoa;
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public int IdTipoOrientacao;
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public DateTime DataRegistro;
    }
}
