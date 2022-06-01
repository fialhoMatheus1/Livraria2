using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Livraria2
{
    class DAO
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public string resultado;
        public int contador;
        public int i;
        public string msg;
        public int[] codigo;// vetor Codigo
        public string[] nome;// vetor nome
        public string[] telefone;// vetor telefone
        public string[] endereco;// vetor endereço
        public DateTime[] data;// vetor data 
        public string[] usuario;// vetor e usuario
        public string[] senha;// vetor senha do usuario

        public DAO() 
        {   //script para conexão do bancos de dados 
            conexao = new MySqlConnection(" server=localhost;DataBase=Livraria2;Uid=root;password=;Convert Zero DateTime=True ");
            try
            {
                conexao.Open();// tentando a conexão com banco de dados 
                Console.WriteLine("  Conectado com sucesso  ");
               

            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                conexao.Close();// fecha a conexão com banco de dados
                
            }




        }//Fim metodo construtor

        public void Inserir(string nome, string telefone, string endereco, DateTime datadenascimento,
                string numeroDoCartao, string chaveDesegurança)
        {
            try
            { // modificar a  estrutura de dados

                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = " @Date ";
                parameter.MySqlDbType = MySqlDbType.Date;
                parameter.Value = datadenascimento.Year + "-" + datadenascimento.Month + "-" + datadenascimento.Day;
                dados = "('','" + nome + "','" + telefone + "','" + endereco + "','" + parameter.Value + "'" +
                    ",'" + numeroDoCartao + "','" + chaveDesegurança + "')";

                comando = " Insert into Cliente(codigo, nome, telefone, endereco, dataDeNascimento, numeroDoCartao, chaveDesegurança) values " + dados;
                // Exercutar o comando de inserção banco de dados

                MySqlCommand sql = new MySqlCommand(comando, conexao);
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
            string query = " select * from Cliente ";
            // instanciar
            codigo = new int[100];
            nome = new string[100];
            telefone = new string[100];
            endereco = new string[100];
            data = new DateTime[100];
            usuario = new string[100];
            senha = new string[100];

            //preencher com valores inciciais 
            for (i = 0; i < 100; i++)
            {
                codigo[i] = 0;
                nome[i] = "";
                telefone[i] = "";
                endereco[i] = "";
                data[i] = new DateTime();
                usuario[i] = "";
                senha[i] = "";

            }//fim do for

            //Creando o consulta do BD-Banco de dados 

            MySqlCommand coletar = new MySqlCommand(query, conexao);

            //Leitura do banco de dados 

            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            contador = 0;
            while (leitura.Read() == true)
            {
                codigo[i] = Convert.ToInt32(leitura["codigo"]);
                nome[i] = leitura["nome"] + "";
                telefone[i] = leitura["telefone"] + "";
                endereco[i] = leitura["endereco"] + "";
                data[i] = Convert.ToDateTime(leitura["dataDeNascimento"]);
                usuario[i] = leitura["usuario"] + "";
                senha[i] = leitura["senha"] + "";
                i++;
                contador++;
            }// fim do while

            // fecha a leitura do banco de dados

            leitura.Close();


        }//fim do medoto de preencher o vetor

        public string ConsultarCliente()
        {
            //preencher os vetores
            preencherVetor();

            msg = "";

            for (i = 0; i < contador; i++)
            {
                msg += "Código:" + codigo[i] +
                       ", Nome: " + nome[i] +
                       ", Telefone: " + telefone[i] +
                       ", Endereco: " + endereco[i] +
                       ", Data De Nascimento: " + data[i] +
                       ", Usuario: " + usuario[i] +
                       ", Senha: " + senha[i] +
                       "\n\n";
            }//fim do for

            return msg;  
        }//fim do metodo consultar cliente

        public string ConsultarCliente(int cod)
        {
            preencherVetor();
            for (i = 0; i < contador; i++)
            {
                if (codigo[i] == cod)
                {
                    msg = "Código: " + codigo[i] +
                    ", Nome: " + nome[i] +
                    ", Telefone: " + telefone[i] +
                    ", Endereco: " + endereco[i] +
                    ", Data De Nascimento: " + data[i] +
                    ", usuario: " + usuario[i] +
                    ", Senha: " + senha[i] +
                    "\n\n";
                    return msg;
                }
            }//fim do for
            return "Código informado não encontrado";
        }//fim do metodo consultar

        public string AtualizarCliente(int cod, string campo, string novoDado)
        {
            try
            {
                string query = " Update Cliente set " + campo + " = '" + novoDado + "' Where codigo = '" + cod + "'";
                //Exercutar o comando
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultadoCliente = "" + sql.ExecuteNonQuery();

                return resultadoCliente + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }
        }//fim do metodo atualizar cliente

        public string DeletarCliente(int cod)
        {
            try
            {
                string query = " Delete from Cliente Where codigo = '" + cod + "'";
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultadoCliente = "" + sql.ExecuteNonQuery();
                // mostrar o resultado em tela
                return resultadoCliente + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }
        }//fim do metodo Deletar cliente
    }// Fim da classe 
}// Fim do projeto
