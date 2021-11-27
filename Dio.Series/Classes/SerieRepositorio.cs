using Dio.Series.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dio.Series
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();

        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public void Insere(Serie objeto)
        {
            listaSerie.Add(objeto);
        }

        public Serie RetornaPorId(int id)
        {
            return listaSerie[id];
        }

        public void Atualiza(int id, Serie objeto)
        {
            listaSerie[id] = objeto;
        }

        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }
    }
}
