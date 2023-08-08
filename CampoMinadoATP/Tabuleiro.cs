using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampoMinadoATP
{
    internal class Tabuleiro
    {
        // Definição de atributos do Tabuleiro
        /* 
         Legenda:
            0 - Indica que o tabuleiro já foi aberto e não há bombas adjacentes
            1 - Indica que o tabuleiro já foi aberto e há uma bomba adjacente
            2 - Indica que o tabuleiro já foi aberto e há duas bombas adjacentes
            X - Indica pontos onde o tabuleiro não foi aberto (tabuleiro original)
        */

        private int linhas;
        private int colunas;
        private int bombas;
        private char[,] tabuleiro_revelado;
        private char[,] tabuleiro_nao_revelado;

        // Metódo com função de criar o tabuleiro de acordo com as linhas e colunas
        // lidas do arquivo de texto.
        public void CriaTabuleiro(int linhas, int colunas)
        {
            this.tabuleiro_revelado = new char[linhas, colunas];
            this.tabuleiro_nao_revelado = new char[linhas, colunas];
            this.linhas = linhas;
            this.colunas = colunas;

            for (int i = 0; i < this.linhas; i++)
            {
                for (int j = 0; j < this.colunas; j++)
                {
                    this.tabuleiro_revelado[i, j] = 'x';
                    this.tabuleiro_nao_revelado[i, j] = 'x';
                }
            }
        }

        // Metódo com função de distribuir as bombas aleatoriamente pelo tabuleiro
        public void DistribuirBombas(int num_bombas)
        {
            int contador = 0;
            this.bombas = num_bombas;
            Random randNumb = new Random();

            while (contador < num_bombas)
            {
                int indice_linha = randNumb.Next(this.linhas);
                int indice_colunas = randNumb.Next(this.colunas);

                if (this.tabuleiro_nao_revelado[indice_linha, indice_colunas] != 'b')
                {
                    this.tabuleiro_nao_revelado[indice_linha, indice_colunas] = 'b';
                    contador++;
                }

            }

        }

        // Metódo com função de distribuir os números pelo tabuleiro. A cada bomba que existir nas casas adjacentes, aumenta em 1 o valor da posição.
        public void DistribuirNumeros()
        {
            for (int i = 0; i < this.linhas; i++)
            {
                for (int j = 0; j < this.colunas; j++)
                {
                    int contador = 0;
                    if (this.tabuleiro_nao_revelado[i, j] != 'b')
                    {
                        // Verifica se as casas SUPERIORES estão dentro dos limites da matriz e se tem bombas nelas. Caso tenha, incrementa 1 no valor do índice.
                        if (i > 0)
                        {
                            if (j > 0 && this.tabuleiro_nao_revelado[i - 1, j - 1] == 'b')
                            {
                                contador++;
                            }
                            if (this.tabuleiro_nao_revelado[i - 1, j] == 'b')
                            {
                                contador++;
                            }
                            if (j < this.colunas - 1 && this.tabuleiro_nao_revelado[i - 1, j + 1] == 'b')
                            {
                                contador++;
                            }
                        }

                        // Verifica se as casas LATERAIS estão dentro dos limites da matriz e se tem bombas nelas. Caso tenha, incrementa 1 no valor do índice.
                        if (j > 0 && this.tabuleiro_nao_revelado[i, j - 1] == 'b')
                        {
                            contador++;
                        }
                        if (j < this.colunas - 1 && this.tabuleiro_nao_revelado[i, j + 1] == 'b')
                        {
                            contador++;
                        }

                        // Verifica se as casas INFERIORES estão dentro dos limites da matriz e se tem bombas nelas. Caso tenha, incrementa 1 no valor do índice.
                        if (i < this.linhas - 1)
                        {
                            if (j > 0 && this.tabuleiro_nao_revelado[i + 1, j - 1] == 'b')
                            {
                                contador++;
                            }
                            if (this.tabuleiro_nao_revelado[i + 1, j] == 'b')
                            {
                                contador++;
                            }
                            if (j < this.colunas - 1 && this.tabuleiro_nao_revelado[i + 1, j + 1] == 'b')
                            {
                                contador++;
                            }
                        }
                    }
                    // Caso encontre uma bomba no índice, passa sobre ele sem dar comando, sem armazenar números nessa casa.
                    else
                    {
                        continue;
                    }

                    this.tabuleiro_nao_revelado[i, j] = (char)(contador + '0');
                }
            }
        }

        // Método utilizado para realizar o processo de liberação da posição escolhida pelo usuário.
        // Verificando se a casa ja está aberta e se há bomba (caso houver, encerrar a aplicação com a derrota do usuário). 
        // Chamando o método de abrir as outras posições de acordo com o tabuleiro
        public bool LiberarPosicao(int linha, int coluna)
        {
            // Verificar se a posição já foi revelada
            if (this.tabuleiro_revelado[linha, coluna] != 'x')
            {
                Console.WriteLine("\nPosição já revelada! Tente novamente!");
                return false;
            }

            // Caso caia em uma bomba, encerrar a aplicação
            if (this.tabuleiro_nao_revelado[linha, coluna] == 'b')
            {
                Console.WriteLine("\nBomba explodiu, você perdeu!");
                return true;
            }

            // Caso a posição nao tiver sido revelada calcular as posicoes adjacentes 
            if (this.tabuleiro_nao_revelado[linha, coluna] == '0')
            {
                this.CalcularPosicoesAdjacentes(linha, coluna);
            }
            else
            {
                // Caso seja um número, apenas liberá-lo
                this.tabuleiro_revelado[linha, coluna] = this.tabuleiro_nao_revelado[linha, coluna];
            }

            return false;
        }

        // Método responsável por abrir as outras posições de acordo com a posição selecionada do tabuleiro
        public void CalcularPosicoesAdjacentes(int linha, int coluna)
        {
            // Verificar se a posição está fora dos limites do tabuleiro ou se a célula já foi revelada
            if (linha < 0 || linha >= this.linhas || coluna < 0 || coluna >= this.colunas || this.tabuleiro_revelado[linha, coluna] != 'x')
            {
                return;
            }

            // Abrir a posição atual
            this.tabuleiro_revelado[linha, coluna] = this.tabuleiro_nao_revelado[linha, coluna];

            // Caso a posição seja 0, verificar em todas as celulas adjacentes se há mais algum 0 ou número que possa ser 
            // liberado. Caso sim, chamo a função novamente, abro a posição e faço o cálculo novamente.
            if (this.tabuleiro_revelado[linha, coluna] == '0')
            {
                // For para percorrer todas as posições adjacentes a posição atual, 
                // sempre verificando se ela não foi liberada, caso não tenha rodando o método novamente.
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int novaLinha = linha + i;
                        int novaColuna = coluna + j;

                        if (novaLinha >= 0 && novaLinha < this.linhas && novaColuna >= 0 && novaColuna < this.colunas)
                        {
                            if (this.tabuleiro_revelado[novaLinha, novaColuna] == 'x')
                            {
                                CalcularPosicoesAdjacentes(novaLinha, novaColuna);
                            }
                        }
                    }
                }
            }
        }

        // Método responsável por verificar se o usuário ganhou. Verificar se todo o tabuleiro foi revelado sem ter explodido alguma bomba.
        public bool VerificarVitoria()
        {
            int contador = 0;

            //Percorre o tabuleiro e verifica quantas posições não abertas ainda existem
            for (int i = 0; i < this.linhas; i++)
            {
                for (int j = 0; j < this.colunas; j++)
                {
                    if (this.tabuleiro_revelado[i, j] == 'x')
                    {
                        contador++;
                    }
                }
            }

            // Se o número de posições não abertas restantes for igual ao número de bombas do tabuleiro, o usuário conseguiu revelar
            // todos os campos numerais sem ter revelado nenhuma das bombas, então é declarada a vitória ao usuário!
            if (contador == bombas)
            {
                Console.WriteLine("\nParabéns, você venceu!");
                return true;
            }

            return false;
        }

        // Metódo com função de exibir o tabuleiro revelado ao usuário, para em seguida efetuar a jogada
        public void MostrarTabuleiroRevelado()
        {
            Console.WriteLine();

            for (int i = 0; i < this.linhas; i++)
            {
                for (int j = 0; j < this.colunas; j++)
                {
                    Console.Write(this.tabuleiro_revelado[i, j] + " | ");
                }

                Console.WriteLine();
            }
        }

        // Metódo com função de exibir o tabuleiro não revelado ao usuário (com as bombas expostas),
        // para exibir ao usuário no momento de GameOver;
        public void MostrarTabuleiroGameOver()
        {
            Console.WriteLine();

            for (int i = 0; i < this.linhas; i++)
            {
                for (int j = 0; j < this.colunas; j++)
                {
                    Console.Write(this.tabuleiro_nao_revelado[i, j] + " | ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nGame over! Obrigado por jogar!");
        }
    }
}
