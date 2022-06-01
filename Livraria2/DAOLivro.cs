using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Livraria2
{
    class DAOLivro
    {
        public MySqlConnection conexaoLivro;
        public string dados;
        public string comando;
        public string resultado;
        public int contadorLivro;
        public int a;
        public string msgLivro;
        public int[] codigoli;// vetor Codigo
        public string[] nomeDoAuto;// vetor nome do autor
        public string[] titulo;// vetor titulo
        public DateTime[] dataDePublicacao;// vetor data de publicação
        public string[] editora;// vetor da editora
        public string[] categoria;// vetor de categoria

        public DAOLivro()
        {
            conexaoLivro = new MySqlConnection(" server=localhost;DataBase=Livraria2;Uid=root;password=;Convert Zero DateTime=True ");
            try
            {
                conexaoLivro.Open();// tentando a conexão com banco de dados 
                Console.WriteLine("  Conectado com sucesso  ");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                conexaoLivro.Close();// fecha a conexão com banco de dados
                Console.ReadLine();
            }
        }//Fim metodo construtor

        public void InserirLivro(string titulo, string nomeDoAutor, DateTime dataDePublicacao,
              string editora, string categoria)
        {
            try
            { // modificar a  estrutura de dados

                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = " @Date ";
                parameter.MySqlDbType = MySqlDbType.Date;
                parameter.Value = dataDePublicacao.Year + "-" + dataDePublicacao.Month + "-" + dataDePublicacao.Day;
                dados = "('','" + titulo + "','" + nomeDoAutor + "','" + editora + "','" + parameter.Value + "'" +
                    ",'" + categoria + "')";

                comando = " Insert into livros(codigoLivros, titulo, nomeDoAutor, dataDePublicacao, editora, categoria) values " + dados;
                // Exercutar o comando de inserção banco de dados

                MySqlCommand sql = new MySqlCommand(comando, conexaoLivro);
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
            string query = " select * from livros ";
            // instanciar
            codigoli = new int[100];
            titulo = new string[100];
            nomeDoAuto = new string[100];
            dataDePublicacao = new DateTime[100];
            editora = new string[100];
            categoria = new string[100];

            //preencher com valores inciciais 
            for (a = 0; a < 100; a++)
            {
                codigoli[a] = 0;
                nomeDoAuto[a] = "";
                titulo[a] = "";
                editora[a] = "";
                dataDePublicacao[a] = new DateTime();
                categoria[a] = "";

            }//fim do for

            //Creando o consulta do BD-Banco de dados 
            MySqlCommand coletar = new MySqlCommand(query, conexaoLivro);
            //Leitura do banco de dados
            MySqlDataReader leitura = coletar.ExecuteReader();
            a = 0;
            contadorLivro = 0;
            while (leitura.Read() == true)
            {
                codigoli[a] = Convert.ToInt32(leitura["codigoLivros"]);
                nomeDoAuto[a] = leitura["nomeDoAutor"] + "";
                titulo[a] = leitura["titulo"] + "";
                editora[a] = leitura["editora"] + "";
                dataDePublicacao[a] = Convert.ToDateTime(leitura["dataDePublicacao"]);
                categoria[a] = leitura["categoria"] + "";
                a++;
                contadorLivro++;
            }// fim do while

            // fecha a leitura do banco de dados

            leitura.Close();

        }
        public string ConsultarLivros()
        {
            //preencher os vetores
            preencherVetor();

            msgLivro = "";

            for (a = 0; a < contadorLivro; a++)
            {
                msgLivro += "Código do Livro:" + codigoli[a] +
                       ", Título: " + titulo[a] +
                       ", Nome do Autor: " + nomeDoAuto[a] +
                       ", Data de Publicação: " + dataDePublicacao[a] +
                       ", Editora: " + editora[a] +
                       ", Categoria: " + categoria[a] +
                       "\n\n";
            }//fim do for
            return msgLivro;
        }//fim do metodo consultar livro

        public string ConsultarLivros(int cod)
        {
            //preencher os vetores
            preencherVetor();

            msgLivro = "";

            for (a = 0; a < contadorLivro; a++)
            {
                if(codigoli[a] == cod)
                {
                    msgLivro = "Código do Livro:" + codigoli[a] +
                       ", Título: " + titulo[a] +
                       ", Nome do Autor: " + nomeDoAuto[a] +
                       ", Data de Publicação: " + dataDePublicacao[a] +
                       ", Editora: " + editora[a] +
                       ", Categoria: " + categoria[a] +
                       "\n\n";
                    return msgLivro;
                }
            }//fim do for
            return "Código informado não encontrado";
        }//fim do metodo consultar livro

        public string AtualizarLivros(int cod, string campo, string novoDado)
        {
            try
            {
                string query = " Update livros set " + campo + " = '" + novoDado + "' Where codigoLivros = '" + cod + "'";
                //Exercutar o comando
                MySqlCommand sql = new MySqlCommand(query, conexaoLivro);
                string resultadoLivro = "" + sql.ExecuteNonQuery();

                return resultadoLivro + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }
        }//fim do metodo atualizar livro

        public string DeletarLivro(int cod)
        {
            try
            {
                string query = " Delete from livros Where codigoLivros = '" + cod + "'";
                MySqlCommand sql = new MySqlCommand(query, conexaoLivro);
                string resultadoLivro = "" + sql.ExecuteNonQuery();
                // mostrar o resultado em tela
                return resultadoLivro + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }
        }//fim do metodo Deletar livro
    }//fim da classe
}//fim do projeto
