using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class AlunoRequest
    {
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public int IdPessoa;
        [Required(ErrorMessage = "A matricula é obrigatório")]
        public string Matricula;
        [Required(ErrorMessage = "O registro ativo é obrigatório")]
        public bool RegistroAtivo;
        [Required(ErrorMessage = "O Curso é obrigatório")]
        public int IdCurso;
    }
}
