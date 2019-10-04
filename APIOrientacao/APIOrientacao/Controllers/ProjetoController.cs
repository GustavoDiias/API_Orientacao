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
    public class ProjetoController : Controller
    {
        private readonly Contexto contexto;

        public ProjetoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            ProjetoRequest projetoRequest)
        {

            var projeto = new Projeto
            {
                Nome = projetoRequest.Nome,
                Encerrado = projetoRequest.Encerrado,
                IdPessoa = projetoRequest.IdPessoa,
                Nota = projetoRequest.Nota
            };

            contexto.Projeto.Add(projeto);
            contexto.SaveChanges();

            var projetoRetorno = contexto.Projeto.Where
                (x => x.IdProjeto == projeto.IdProjeto)
                .FirstOrDefault();

            ProjetoResponse response = new ProjetoResponse();

            if (projetoRetorno != null)
            {
                response.IdProjeto = projetoRetorno.IdProjeto;
                response.Nome = projetoRetorno.Nome;
                response.Encerrado = projetoRetorno.Encerrado;
                response.IdPessoa = projetoRetorno.IdPessoa;
                response.Nota = projetoRetorno.Nota;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idProjeto}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        public IActionResult Get(int idProjeto)
        {
            var projeto = contexto.Projeto.FirstOrDefault(
                x => x.IdProjeto == idProjeto);

            return StatusCode(projeto == null
                ? 404 :
                200, new ProjetoResponse
                {
                    //Vai Da Merda 2
                    IdProjeto = projeto == null ? -1 : projeto.IdProjeto,
                    Nome = projeto == null ? "Nome não encontrado"
                    : projeto.Nome,
                    Encerrado= projeto == null ? false : projeto.Encerrado,
                    IdPessoa = projeto == null ? -1 : projeto.IdPessoa,
                    Nota = projeto == null ? 10 : projeto.Nota
                });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] ProjetoRequest projetoRequest)
        {
            try
            {
                var projeto = contexto.Projeto.Where(x => x.IdProjeto == id)
                        .FirstOrDefault();

                if (projeto != null)
                {
                    projeto.Nome = projetoRequest.Nome;
                    projeto.Encerrado = projetoRequest.Encerrado;
                    projeto.IdPessoa = projetoRequest.IdPessoa;
                    projeto.Nota = projetoRequest.Nota;
                }

                contexto.Entry(projeto).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

                var projetoRetorno = contexto.Projeto.FirstOrDefault
                (
                    x => x.IdProjeto == id
                );

                ProjetoResponse retorno = new ProjetoResponse()
                {
                    IdProjeto = projetoRetorno.IdProjeto,
                    Nome = projetoRetorno.Nome,
                    Encerrado = projetoRetorno.Encerrado,
                    IdPessoa = projetoRetorno.IdPessoa,
                    Nota = projetoRetorno.Nota
                };

                return StatusCode(200, retorno);
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.
                    Message.FirstOrDefault());
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                var projeto = contexto.Projeto.FirstOrDefault(
                    x => x.IdProjeto == id);

                if (projeto != null)
                {
                    contexto.Projeto.Remove(projeto);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Projeto excluído com sucesso!");
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message
                    .FirstOrDefault());
            }
        }

    }
}