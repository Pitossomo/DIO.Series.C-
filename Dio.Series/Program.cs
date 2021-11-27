using Dio.Series;
using Dio.Series.Enum;

namespace HelloWorld
{
    class Program
    {

        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {

            string opcaoUsuario = "";

            while (opcaoUsuario.ToUpper() != "X")
            {
                opcaoUsuario = ObterOpcaoUsuario();
                switch (opcaoUsuario)
                {
                    case "1":   // Lista as séries no repositório
                        ListarSeries();
                        break;

                    case "2":   // Insere nova série
                        InserirSerie();
                        break;

                    case "3":   // Visualiza detalhes de uma série
                        VisualizaSerie();
                        break;
                            
                    case "4":   // Atualiza os dados de uma série
                        AtualizaSerie();
                        break;

                    case "5":   // Exclui uma série
                        ExcluiSerie();
                        break;

                    case "C":   // Limpa tela
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Opção não disponível. Tente novamente.");
                        break;
                }

            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu dispor!!!");

            Console.WriteLine();
            Console.WriteLine("Escolha uma opção, digite o número correspondente e tecle Enter:");
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Visualizar série");
            Console.WriteLine("4 - Atualizar série");
            Console.WriteLine("5 - Excluir série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");

            Console.Write("Opção: ");
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void ListarSeries()     // Imprime na tela os títulos de todas as séries
        {
            var lista = repositorio.Lista();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada :(");
                return;
            }
            foreach (var serie in lista)
            {
                Console.WriteLine();
                Console.WriteLine("#{0}:{1}{2}",
                    serie.RetornaId(),
                    serie.RetornaExcluido() ? " Excluído - " : " ",
                    serie.RetornaTitulo()
                );

            }
            Console.WriteLine();
        }

        private static void InserirSerie()     // Insere nova série, após pedir ao usuário seu gênero, título e ano
        {
            Console.WriteLine();

            ListaGeneros();
            int genero;
            bool generoValido;
            do
            {
                Console.Write("Insira o gênero da nova série: ");
                genero = LerInt();
                generoValido = Enum.IsDefined(typeof(Genero), genero);
                if (!generoValido) Console.WriteLine("Gênero inválido. Informe outra opção");
            } while (!generoValido);

            Console.Write("Insira o título da nova série: ");
            string titulo = Console.ReadLine();

            Console.Write("Insira o ano da nova série: ");
            int ano = LerInt();

            Console.Write("Insira uma descrição da nova série: ");
            string descricao = Console.ReadLine();

            Serie novaSerie = new Serie(repositorio.ProximoId(), (Genero)genero, titulo, descricao, ano);
            repositorio.Insere(novaSerie);
        }

        private static void VisualizaSerie()    // Visualiza os detalhes de uma série
        {
            if (repositorio.Lista().Count() == 0)
            {
                Console.WriteLine("Nenhuma série para ser visualizada. Use a opção 2 para inserir uma nova série");
                return;
            };

            Console.WriteLine("");
            Console.Write("Insira o ID da série a ser detalhada: ");
            int idSerie = LerId();
            Console.WriteLine(repositorio.RetornaPorId(idSerie));
        }

        private static void AtualizaSerie() 
        {
            if (repositorio.Lista().Count() == 0)
            {
                Console.WriteLine("Nenhuma série para ser atualizada. Use a opção 2 para inserir uma nova série");
                return;
            };

            Console.WriteLine();
            Console.Write("Insira o id da série a ser atualizada: ");
            int id = LerId();

            ListaGeneros();
            Console.Write("Insira o novo gênero da série: ");
            int genero = LerInt();

            Console.Write("Insira o novo título da nova série: ");
            string titulo = Console.ReadLine();

            Console.Write("Insira o novo ano da série: ");
            int ano = LerInt();

            Console.Write("Insira a nova descrição da série: ");
            string descricao = Console.ReadLine();

            Serie serieAtualizada = new Serie(id, (Genero)genero, titulo, descricao, ano);
            repositorio.Atualiza(id, serieAtualizada);
        }
    
        private static void ExcluiSerie()
        {
            if (repositorio.Lista().Count() == 0)
            {
                Console.WriteLine("Nenhuma série a ser excluída. Use a opção 2 para inserir uma nova série");
                return;
            };

            Console.WriteLine();
            Console.Write("Insira o ID da série a ser excluida: ");
            int idSerie = LerId();
            repositorio.RetornaPorId(idSerie).Excluir();
            Console.WriteLine("Série excluída.");
        }

        private static void ListaGeneros()
        {
            // Lista os gêneros disponíveis
            Console.WriteLine(" * GÊNEROS DE SÉRIES * ");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
        }

        private static int LerId()
        {
            int idSerie;
            bool idValido;
            // Repete para o usuario inserir um ID até que ele entre com um valor válido
            do
            {
                idSerie = LerInt();
                idValido = (idSerie < repositorio.ProximoId() && idSerie >= 0) ? true : false;
                if (!idValido) Console.WriteLine("Valor inválido. Informe um id existente");
            } while (!idValido);
            return idSerie;
        }

        private static int LerInt()
        {
            int entrada;
            // Enquanto não for informado um número inteiro válido, continue pedindo para entrar um valor inteiro
            while (!int.TryParse(Console.ReadLine(), out entrada))
            {
                Console.WriteLine("Por favor, informe um número inteiro");
            }
            return entrada;
        }
    }
}