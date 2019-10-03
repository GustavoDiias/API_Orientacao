﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIOrientacao.Data.Context
{
    public class Contexto : DbContext
    {
        public Contexto()
        { }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Situacao> Situacao { get; set; }
        public DbSet<SituacaoProjeto> SituacaoProjeto { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<TipoOrientacao> TipoOrientacao { get; set; }
        public DbSet<Orientacao> Orientacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Remover a propriedade de restrição-validação de exclusão
                // em cascata
            foreach (var relacionamento in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relacionamento.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("dbo");

            #region Pessoa
            modelBuilder.Entity<Pessoa>(e =>
            {
                e.ToTable("Pessoa");
                e.HasKey(c => c.IdPessoa).HasName("IdPessoa");
                e.Property(c => c.IdPessoa).HasColumnName("IdPessoa")
                .ValueGeneratedOnAdd();

                e.Property(c => c.Nome).HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(300);
            });
            #endregion

            #region Curso
            modelBuilder.Entity<Curso>(e =>
            {
                e.ToTable("Curso");
                e.HasKey(c => c.IdCurso).HasName("IdCurso");
                e.Property(c => c.IdCurso).HasColumnName("IdCurso")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Nome).HasColumnName("Nome")
                    .IsRequired()
                    .HasMaxLength(150);

            });
            #endregion

            #region Aluno
            modelBuilder.Entity<Aluno>(e =>
            {
                e.ToTable("Aluno");
                e.HasKey(c => c.IdPessoa).HasName("IdPessoaAluno");
                e.Property(c => c.IdPessoa).HasColumnName("IdPessoa")
                    .IsRequired();
                e.Property(c => c.IdCurso).HasColumnName("IdCurso")
                    .IsRequired();

                e.Property(c => c.Matricula)
                .HasColumnName("Matricula")
                .IsRequired()
                .HasMaxLength(8);

                e.Property(c => c.RegistroAtivo)
                .HasColumnName("RegistroAtivo")
                .IsRequired();

                e.HasOne(d => d.Pessoa)
                    .WithOne(p => p.Aluno)
                    .HasForeignKey<Aluno>(d => d.IdPessoa)
                    .HasConstraintName("PFK_PessoaAluno");

                e.HasOne(d => d.Curso)
                    .WithMany(p => p.Alunos)
                    .HasForeignKey(d => d.IdCurso)
                    .HasConstraintName("FK_CursoAluno");
            });
            #endregion

            #region Situação
            modelBuilder.Entity<Situacao>(e =>
            {
                e.ToTable("Situacao");
                e.HasKey(c => c.IdSituacao).HasName("IdSituacao");
                e.Property(c => c.IdSituacao).HasColumnName("IdSituacao")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Descricao).HasColumnName("Descricao")
                    .IsRequired()
                    .HasMaxLength(100);
            });
            #endregion

            #region Projeto
            modelBuilder.Entity<Projeto>(e =>
            {
                e.ToTable("Projeto");
                e.HasKey(k => k.IdProjeto).HasName("IdProjeto");
                e.Property(c => c.IdProjeto).HasColumnName("IdProjeto")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.IdPessoa).HasColumnName("IdPessoa")
                .IsRequired();

                e.Property(c => c.Encerrado).HasColumnName("Encerrado")
                    .IsRequired();
                e.Property(c => c.Nota).HasColumnName("Nota").IsRequired();

                e.Property(c => c.Nome).HasColumnName("Nome").IsRequired().
                HasMaxLength(200);

                e.HasOne(d => d.Aluno)
                    .WithMany(p => p.Projetos)
                    .HasForeignKey(d => d.IdPessoa)
                    .HasConstraintName("FK_AlunoProjeto");
            });
            #endregion

            #region SituaçãoProjeto
            modelBuilder.Entity<SituacaoProjeto>(e =>
            {
                e.ToTable("SituacaoProjeto");
                e.HasKey(k => new { k.IdProjeto, k.IdSituacao })
                .HasName("FK_SituacaoProjeto");

                e.Property(c => c.IdProjeto).HasColumnName("IdProjeto")
                .IsRequired();

                e.Property(c => c.IdSituacao).HasColumnName("IdSituacao")
                .IsRequired();

                e.Property(c => c.DataRegistro).HasColumnName("DataRegistro")
                .HasColumnType("datetime").IsRequired();

                e.HasOne(d => d.Situacao)
                    .WithMany(p => p.SituacoesProjeto)
                    .HasForeignKey(d => d.IdSituacao)
                    .HasConstraintName("FK_SituacaoSituacaoProjeto");

                e.HasOne(d => d.Projeto)
                    .WithMany(p => p.SituacoesProjeto)
                    .HasForeignKey(d => d.IdProjeto)
                    .HasConstraintName("FK_ProjetoSituacaoProjeto");
            });
            #endregion

            #region Professor
            modelBuilder.Entity<Professor>(e =>
            {
                e.ToTable("Professor");
                e.HasKey(c => c.IdPessoa).HasName("IdPessoaProfessor");
                e.Property(c => c.IdPessoa).HasColumnName("IdPessoa")
                    .IsRequired();

                e.Property(c => c.RegistroAtivo)
                .HasColumnName("RegistroAtivo")
                .IsRequired();

                e.HasOne(d => d.Pessoa)
                    .WithOne(p => p.Professor)
                    .HasForeignKey<Professor>(d => d.IdPessoa)
                    .HasConstraintName("PFK_PessoaProfessor");

            });
            #endregion

            #region TipoOrientacao
            modelBuilder.Entity<TipoOrientacao>(e =>
            {
                e.ToTable("TipoOrientacao");
                e.HasKey(c => c.IdTipoOrientacao).HasName("IdTipoOrientacao");
                e.Property(c => c.IdTipoOrientacao).HasColumnName("IdTipoOrientacao")
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Descricao).HasColumnName("Descricao")
                    .IsRequired()
                    .HasMaxLength(300);

            });
            #endregion

            #region Orientação
            modelBuilder.Entity<Orientacao>(e =>
            {
                e.ToTable("Orientacao");
                e.HasKey(k => new { k.IdProjeto, k.IdPessoa })
                .HasName("FK_Orientacao");

                e.Property(c => c.IdProjeto).HasColumnName("IdProjeto")
                .IsRequired();

                e.Property(c => c.IdPessoa).HasColumnName("IdPessoa")
                .IsRequired();

                e.Property(c => c.IdTipoOrientacao).HasColumnName("IdTipoOrientacao")
                .IsRequired();

                e.Property(c => c.DataRegistro).HasColumnName("DataRegistro")
                .HasColumnType("datetime").IsRequired();

                e.HasOne(d => d.TipoOrientacao)
                .WithMany(p => p.Orientacoes)
                .HasForeignKey(d => d.IdTipoOrientacao)
                .HasConstraintName("FK_TipoOrientacaoOrientacao");

                e.HasOne(d => d.Projeto)
                    .WithMany(p => p.Orientacoes)
                    .HasForeignKey(d => d.IdProjeto)
                    .HasConstraintName("FK_ProjetoOrientacao");

                e.HasOne(d => d.Professor)
                    .WithMany(p => p.Orientacoes)
                    .HasForeignKey(d => d.IdPessoa)
                    .HasConstraintName("FK_PessoaOrientacao");        
            });
            #endregion
        }
    }
}
