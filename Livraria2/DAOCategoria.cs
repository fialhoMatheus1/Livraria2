using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;



namespace Livraria2
{
    class DAOCategoria
    {
        public MySqlConnection conexaoCategoria;
        public string dados;
        public string comando;
        public string resultado;
        public int[] codigoCategoria;
        public string[] descriscao;
        public int e;
        public int contadorCategoria;
        public string msgCategoria;

        public DAOCategoria()
        {
            conexaoCategoria = new MySqlConnection(" server=localhost;DataBase=Livraria2;Uid=root;password=;Convert Zero DateTime=True ");
            try
            {
                conexaoCategoria.Open();// tentando a conexão com banco de dados
                Console.WriteLine(" Conectado com sucesso ");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                conexaoCategoria.Close();// fecha a conexão com banco de dados
                Console.ReadLine();
            }
        }//fim do metodo construtor

        public void InserirCategoria(string usuario, string senha)
        {
            try
            { // modificar a estrutura de dados
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = " @Date ";
                parameter.MySqlDbType = MySqlDbType.Date;
                comando = " Insert into Categoria(codigoCategoria, descriscao) values " + dados;
                // Exercutar o comando de inserção banco de dados

                MySqlCommand sql = new MySqlCommand(comando, conexaoCategoria);
                resultado = " " + sql.ExecuteNonQuery();// Executa o insert no BD
                Console.WriteLine(resultado + " Linhas Afetadas ");
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                Console.ReadLine();// manter o prompt aberto
            }// fim do metodo construtor
        }//fim do metodo inserir categoria

        public void preencherVetor()
        {
            string query = " select * from Categoria ";
            // instanciar
            codigoCategoria = new int[100];
            descriscao = new string[100];

            //preencher com valores inciciais
            for (e = 0; e < 100; e++)
            {
                codigoCategoria[e] = 0;
                descriscao[e] = "";
            }//fim do for

            //Creando o consulta do BD-Banco de dados
            MySqlCommand coletar = new MySqlCommand(query, conexaoCategoria);
            //Leitura do banco de dados
            MySqlDataReader leitura = coletar.ExecuteReader();
            e = 0;
            contadorCategoria = 0;
            while (leitura.Read() == true)
            {
                codigoCategoria[e] = Convert.ToInt32(leitura["codigoCategoria"]);
                descriscao[e] = leitura["quantidade"] + "";
                e++;
                contadorCategoria++;
            }// fim do while

            //fecha a leitura do banco de dados
            leitura.Close();
        }//fim do metodo preencher vetor

        public string ConsultarCategoria()
        {
            //preencher os vetores
            preencherVetor();
            msgCategoria = "";

            for (e = 0; e < contadorCategoria; e++)
            {
                msgCategoria += "Código da Categoria:" + codigoCategoria[e] +
                ", Descrição: " + descriscao[e] +
                "\n\n";
            }//fim do for
            return msgCategoria;
        }//fim do metodo consultar categoria

        public string ConsultarCategoria(int cod)
        {
            //preencher os vetores
            preencherVetor();
            msgCategoria = "";

            for (e = 0; e < contadorCategoria; e++)
            {
                if (codigoCategoria[e] == cod)
                {
                    msgCategoria = "Código da Categoria:" + codigoCategoria[e] +
                    ", Descrição: " + descriscao[e] +
                    "\n\n";
                    return msgCategoria;
                }
            }//fim do for
            return "Código informado não encontrado";
        }//fim do metodo consultar categoria

        public string AtualizarCategoria(int cod, string campo, string novoDado)
        {
            try
            {
                string query = " Update Categoria set " + campo + " = '" + novoDado + "' Where codigoCategoria = '" + cod + "'";
                //Exercutar o comando
                MySqlCommand sql = new MySqlCommand(query, conexaoCategoria);
                string resultadoCategoria = "" + sql.ExecuteNonQuery();
                return resultadoCategoria + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }//fim do catch
        }//fim do metodo atualizar categoria

        public string DeletarCategoria(int cod)
        {
            try
            {
                string query = " Delete from Categoria Where codigoCategoria = '" + cod + "'";
                MySqlCommand sql = new MySqlCommand(query, conexaoCategoria);
                string resultadoCategoria = "" + sql.ExecuteNonQuery();
                // mostrar o resultado em tela
                return resultadoCategoria + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }
        }//fim do metodo Deletar categoria
    }//fim da classe
}//fim do projeto