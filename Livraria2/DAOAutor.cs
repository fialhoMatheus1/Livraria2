using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Livraria2
{
    class DAOAutor
    {
        public MySqlConnection conexaoAutor;
        public string dados;
        public string comando;
        public string resultado;
        public int contadorAutor;
        public int d;
        public string msgAutor;
        public int[] codigoAo;// vetor Codigo
        public string[] nomeAutor;// vetor nome do autor

        public DAOAutor()
        {
            conexaoAutor = new MySqlConnection(" server=localhost;DataBase=Livraria2;Uid=root;password=;Convert Zero DateTime=True ");
            try
            {
                conexaoAutor.Open();// tentando a conexão com banco de dados 
                Console.WriteLine("  Conectado com sucesso  ");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                conexaoAutor.Close();// fecha a conexão com banco de dados
                Console.ReadLine();
            }
        }//Fim metodo construtor

        public void InserirAutor(string nomeDoAutor)
        {
            try
            { // modificar a  estrutura de dados


                dados = "('','" + nomeDoAutor + "')";

                comando = " Insert into Autor(codigoAutor, nomeDoAutor) values " + dados;
                // Exercutar o comando de inserção banco de dados

                MySqlCommand sql = new MySqlCommand(comando, conexaoAutor);
                resultado = " " + sql.ExecuteNonQuery();// Executa o insert no BD
                Console.WriteLine(resultado + " Linhas Afetadas ");
            }

            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                Console.ReadLine();// manter o prompt aberto

            }

        }//fim do metodo construdor 
        public void preencherVetor()
        {
            string query = " select * from Autor ";
            // instanciar
            codigoAo = new int[100];
            nomeAutor = new string[100];


            //preencher com valores inciciais 
            for (d = 0; d < 100; d++)
            {
                codigoAo[d] = 0;
                nomeAutor[d] = "";


            }//fim do for
            //Creando o consulta do BD-Banco de dados 

            MySqlCommand coletar = new MySqlCommand(query, conexaoAutor);

            //Leitura do banco de dados 
            
            MySqlDataReader leitura = coletar.ExecuteReader();

            d = 0;
            contadorAutor = 0;
            while (leitura.Read() == true)
            {
                codigoAo[d] = Convert.ToInt32(leitura["codigoAutor"]);
                nomeAutor[d] = leitura["nomeDoAutor"] + "";
                
            }// fim do while

            // fecha a leitura do banco de dados

            leitura.Close();

        }//fim do preencher vertor
        public string consultarAutor(int cod)
        {
            preencherVetor();
            for (d = 0; d < contadorAutor; d++)
            {
                if (codigoAo[1] == cod)
                {
                    msgAutor = "codigo do autor: " + codigoAo[d] +
                     " nome do autor: " + nomeAutor[d] +
                     "\n\n ";
                    return msgAutor;
                }

            }// fim do for
            return " codigo informando não encontrado! ";

        }//fim consultar do Autor

        public string AtualizarAutor(int cod, string campoAo, string novoDadoAo)
        {
            try
            {
                string query = " Update Autor set " + campoAo + " = '" + novoDadoAo + "' Where codigoAutor = '" + cod + "'";
                //Exercutar o comando
                MySqlCommand sql = new MySqlCommand(query, conexaoAutor);
                string resultado = "" + sql.ExecuteNonQuery();

                return resultado + "linha Alterada";

            }
            catch (Exception e)
            {

                return "Algo esta errado " + e;

            }//fim do catch
        }//fim do metodo atualizar compra

        public string DeletarAutor(int cod)
        {
            try
            {
                string query = " Delete from Autor Where codigoAutor = '" + cod + "'";
                MySqlCommand sql = new MySqlCommand(query, conexaoAutor);
                string resultadoAutor = "" + sql.ExecuteNonQuery();
                // mostrar o resultado em tela
                return resultadoAutor + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }
        }//fim do metodo Deletar cliente


    }//fim classe
}// fim do projeto
