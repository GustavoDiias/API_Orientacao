using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class ProfessorRequest
    {
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public int IdPessoa;
        [Required(ErrorMessage = "O Registro Ativo é obrigatório")]
        public bool RegistroAtivo;

    }
}
