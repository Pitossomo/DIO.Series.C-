using Dio.Series.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dio.Series
{
    public class Serie : EntidadeBase
    {
        // Atributos
        private Genero Genero { get; set; }
        private string Titulo { get; set; }
        private string Descricao { get; set; }
        private int Ano { get; set; }
        private bool Excluido { get; set; }

        // Métodos
        public Serie(int id, Genero genero, string titulo, string descricao, int ano)
        {
            this.Id = id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }

        public void Excluir()
        {
            this.Excluido = true;
        }

        public string RetornaTitulo() 
        {
            return this.Titulo;
        }
        public int RetornaId()
        {
            return this.Id;
        }
        public bool RetornaExcluido()
        {
            return this.Excluido;
        }
        public override string ToString()
        {
            // Environment.NewLine - reconhece a nova linha dependendo do sistema operacional
            string retorno = (this.Excluido ? "--- *** --- Excluído --- *** ---" : "################################") + Environment.NewLine;
            retorno += "### Gênero: " + this.Genero + Environment.NewLine;
            retorno += "### Título: " + this.Titulo + Environment.NewLine;
            retorno += "### Descrição: " + this.Descricao + Environment.NewLine;
            retorno += "### Ano de Início: " + this.Ano + Environment.NewLine;
            retorno += "################################" + Environment.NewLine;
            return retorno;
        }
    }
}
