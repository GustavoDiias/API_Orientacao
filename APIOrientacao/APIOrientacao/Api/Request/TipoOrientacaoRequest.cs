﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class TipoOrientacaoRequest
    {
        [Required(ErrorMessage = "A Pessoa é obrigatório")]
        public string Descricao;
    }
}
