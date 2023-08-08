/*
 * Nosso programa desenvolverá uma partida de campo minado no c#. 
 * Programadores: Miguel Oliveira Bizzi, Samuel Leite Diniz
 * Data de escrita: 17/06
 * Ultima atualização: 21/06
 */

namespace CampoMinadoATP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Instanciando nossas classes

            Tabuleiro tabuleiro = new Tabuleiro();
            ArquivoTexto arquivo = new ArquivoTexto();

            /* Primeiramente, iremos pegar o número de linhas, colunas e de bombas que o usuário desejar e criar um
            arquivo de texto externo usando a classe StreamWriter. Em seguinda, usando a classe StreamReader, lemos as informações
            contidas no arquivo texto e pegamos somente os valores necessários para criarmos o tabuleiro*/

            Console.WriteLine("Digite o número de linhas, colunas e bombas do tabuleiro desejado, respectivamente!");
            int linhas = int.Parse(Console.ReadLine());
            int colunas = int.Parse(Console.ReadLine());
            int bombas = int.Parse(Console.ReadLine());

            if (linhas <= 1 || colunas <= 1 || bombas <= 0)
            {
                Console.WriteLine("Entrada inválida! Encerrando aplicação");
                return;
            }

            arquivo.CriarArquivoTexto(linhas, colunas, bombas);
            (linhas, colunas, bombas) = arquivo.LerArquivoTexto();

            // Validando se informacoes vindas da leitura do arquivo de texto estao corretas (não entrou no catch)
            if (linhas == 0 || colunas == 0 || bombas == 0)
            {
                Console.WriteLine("Encerrando aplicação");
            }

            // Momento em que chamamos os métodos de criação do tabuleiro, no qual recebem os valores digitados pelo usuário e cria de acordo com estes.
            // Distribuímos as bombas aleatóriamente e os números que indicam se existem bombas em volta da posição.

            tabuleiro.CriaTabuleiro(linhas, colunas);
            tabuleiro.DistribuirBombas(bombas);
            tabuleiro.DistribuirNumeros();

            // Damos início ao nosso jogo, usando um loop, pedindo para que o usuário faça uma nova jogada até que termine a partida.

            bool isGameOver = false;
            while (!isGameOver)
            {

                tabuleiro.MostrarTabuleiroRevelado();

                // Lê os valores de linha e coluna digitados pelo usuário e armazena nas variáveis acima, registrando a jogada do usuário.
                // Verifica se o usuário digitou uma entrada válida, que esteja dentro dos limites da matriz.

                Console.WriteLine("\nFaça sua jogada\nDigite a linha e coluna que deseja jogar");
                int linha_selected = int.Parse(Console.ReadLine());
                int coluna_selected = int.Parse(Console.ReadLine());

                if (linha_selected < 0 || linha_selected >= linhas || coluna_selected < 0 || coluna_selected >= colunas)
                {
                    Console.WriteLine("\nEntrada inválida! Por favor, digite um valor dentro do limite do tabuleiro.");
                    continue;
                }

                // Chamamos o método liberarPosição que recebe as variáveis com a jogada do usuário e revela a posição escolhida no tabuleiro.

                isGameOver = tabuleiro.LiberarPosicao(linha_selected, coluna_selected);

                // Aqui verificamos se o usuário venceu. Chamamos o método VerificarVitoria, que é armazenado em isGameOver, dando fim a partida.

                bool isWin = tabuleiro.VerificarVitoria();
                if (isWin == true)
                {
                    isGameOver = isWin;
                }
                else
                {
                    continue;
                }

            }

            // Partida encerrou, então exibiremos o Tabuleiro com as bombas e números a mostra.

            tabuleiro.MostrarTabuleiroGameOver();
        }
    }
}