using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIOrientacao.Api.Request;
using APIOrientacao.Api.Response;
using APIOrientacao.Data;
using APIOrientacao.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace APIOrientacao.Controllers
{
    [Route("api/[controller]")]
    public class OrientacaoController : Controller
    {
        private readonly Contexto contexto;

        public OrientacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            OrientacaoRequest orientacaoRequest)
        {

            var orientacao = new Orientacao
            {
                IdTipoOrientacao = orientacaoRequest.IdTipoOrientacao,
                DataRegistro = orientacaoRequest.DataRegistro
            };

            contexto.Orientacao.Add(orientacao);
            contexto.SaveChanges();

            var orientacaoRetorno = contexto.Orientacao.Where
                (x => x.IdProjeto == orientacao.IdProjeto && x.IdPessoa == orientacao.IdPessoa)
                .FirstOrDefault();

            OrientacaoResponse response = new OrientacaoResponse();

            if (orientacaoRetorno != null)
            {
                response.IdProjeto = orientacaoRetorno.IdProjeto;
                response.IdPessoa = orientacaoRetorno.IdPessoa;
                response.IdTipoOrientacao = orientacaoRetorno.IdTipoOrientacao;
                response.DataRegistro = orientacaoRetorno.DataRegistro;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idProjeto}/{idPessoa}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        public IActionResult Get(int idProjeto, int idPessoa)
        {
            var orientacao = contexto.Orientacao.FirstOrDefault(
                x => x.IdProjeto == idProjeto && x.IdPessoa == idPessoa);

            return StatusCode(orientacao == null
                ? 404 :
                200, new OrientacaoResponse
                {
                    IdProjeto = orientacao == null ? -1 : orientacao.IdProjeto,
                    IdPessoa = orientacao == null ? -1 : orientacao.IdPessoa,
                    IdTipoOrientacao = orientacao == null ? -1 : orientacao.IdTipoOrientacao,
                    DataRegistro = orientacao == null ? DateTime.Now : orientacao.DataRegistro
                });
        }

        [HttpPut("{id}/{id2}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id,int id2, [FromBody] OrientacaoRequest orientacaoRequest)
        {
            try
            {
                var orientacao = contexto.Orientacao.Where(x => x.IdProjeto == id && x.IdPessoa == id2)
                        .FirstOrDefault();

                if (orientacao != null)
                {
                    orientacao.IdTipoOrientacao = orientacaoRequest.IdTipoOrientacao;
                    orientacao.DataRegistro = orientacaoRequest.DataRegistro;
                }

                contexto.Entry(orientacao).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var orientacaoRetorno = contexto.Orientacao.FirstOrDefault
                (
                    x => x.IdProjeto == id && x.IdPessoa == id2
                );

                OrientacaoResponse retorno = new OrientacaoResponse()
                {
                    IdProjeto = orientacaoRetorno.IdProjeto,
                    IdPessoa = orientacaoRetorno.IdPessoa,
                    IdTipoOrientacao = orientacaoRetorno.IdTipoOrientacao,
                    DataRegistro = orientacaoRetorno.DataRegistro
                };

                return StatusCode(200, retorno);
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.
                    Message.FirstOrDefault());
            }

        }

        [HttpDelete("{id}/{id2}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id,int id2)
        {
            try
            {
                var orientacao = contexto.Orientacao.FirstOrDefault(
                    x => x.IdProjeto == id && x.IdPessoa == id2);

                if (orientacao != null)
                {
                    contexto.Orientacao.Remove(orientacao);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Orientacao excluída com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}