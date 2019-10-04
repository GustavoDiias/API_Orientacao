using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class SituacaoProjetoRequest
    {
        [Required(ErrorMessage = "A Situacao é obrigatório")]
        public int IdSituacao;
        [Required(ErrorMessage = "O Projeto é obrigatório")]
        public int IdProjeto;
        [Required(ErrorMessage = "A Data Registro é obrigatório")]
        public DateTime DataRegistro;
    }
}
