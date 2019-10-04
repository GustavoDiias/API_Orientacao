using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class ProjetoRequest
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome;
        [Required(ErrorMessage = "O Encerrado é obrigatório")]
        public bool Encerrado;
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public int IdPessoa;
        [Required(ErrorMessage = "A Nota é obrigatório")]
        public decimal Nota;
    }
}
