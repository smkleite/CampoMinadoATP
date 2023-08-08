using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampoMinadoATP
{
    internal class ArquivoTexto
    {
        private string caminho = "info_tabuleiro.txt";

        //Criando arquivo texto e escrevendo as informações do tabuleiro: linhas, colunas e bombas.
        public void CriarArquivoTexto(int linhas, int colunas, int bombas)
        {
            try
            {
                using (StreamWriter arquivo = new StreamWriter(caminho))
                {
                    arquivo.WriteLine("Número de linhas: " + linhas);
                    arquivo.WriteLine("Número de colunas: " + colunas);
                    arquivo.WriteLine("Número de bombas: " + bombas);

                    arquivo.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Arquivo não encontrado!");
            }
        }

        /* Lendo arquivo texto e percorrendo cada uma das frases (uma em cada linha do arquivo) e verificamos qual informação tem nela.
        * Se for o número de linhas, por exemplo, dividimos a frase a partir do ":" e pegamos o segundo índice, que é o número que queremos. */
        public (int, int, int) LerArquivoTexto()
        {
            try
            {
                string linhas_string = "";
                string colunas_string = "";
                string bombas_string = "";

                using (StreamReader arquivo = new StreamReader(caminho))
                {
                    string frase;
                    while ((frase = arquivo.ReadLine()) != null)
                    {
                        if (frase.StartsWith("Número de linhas:"))
                        {
                            linhas_string = frase.Split(':')[1].Trim();
                        }
                        else if (frase.StartsWith("Número de colunas:"))
                        {
                            colunas_string = frase.Split(':')[1].Trim();
                        }
                        else if (frase.StartsWith("Número de bombas:"))
                        {
                            bombas_string = frase.Split(':')[1].Trim();
                        }
                    }

                    int num_linhas = int.Parse(linhas_string);
                    int num_colunas = int.Parse(colunas_string);
                    int num_bombas = int.Parse(bombas_string);


                    arquivo.Close();
                    return (num_linhas, num_colunas, num_bombas);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Arquivo não encontrado!");
                return (0, 0, 0);
            }
        }
    }
}
