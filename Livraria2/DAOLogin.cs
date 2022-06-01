using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Livraria2
{
    class DAOLogin
    {
        public MySqlConnection conexaoLogin;
        public string dados;
        public string comando;
        public string resultado;
        public string[] usuario;
        public string[] senha;
        public int[] codigousuario;
        public int c;
        public int contadorLogin;
        public string msgLogin;
        public string msgValidacao;

        public DAOLogin()
        {
            conexaoLogin = new MySqlConnection(" server=localhost;DataBase=Livraria2;Uid=root;password=;Convert Zero DateTime=True ");
            try
            {
                conexaoLogin.Open();// tentando a conexão com banco de dados
                Console.WriteLine(" Conectado com sucesso ");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                conexaoLogin.Close();// fecha a conexão com banco de dados
                Console.ReadLine();
            }
        }//fim do metodo construtor

        public void InserirLogin(string usuario, string senha)
        {
            try
            { // modificar a estrutura de dados
                dados = "('','" + usuario + "','" + senha + "')";

                comando = " Insert into usuario(codigousuario, usuario, senha) values " + dados;
                // Exercutar o comando de inserção banco de dados

                MySqlCommand sql = new MySqlCommand(comando, conexaoLogin);
                resultado = " " + sql.ExecuteNonQuery();// Executa o insert no BD
                Console.WriteLine(resultado + " Linhas Afetadas ");
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                Console.ReadLine();// manter o prompt aberto
            }// fim do metodo construtor
        }//fim do metodo inserir compra

        public void preencherVetor()
        {
            string query = " select * from usuario ";
            // instanciar
            codigousuario = new int[100];
            usuario = new string[100];
            senha = new string[100];

            //preencher com valores inciciais
            for (c = 0; c < 100; c++)
            {
                codigousuario[c] = 0;
                usuario[c] = "";
                senha[c] = "";
            }//fim do for

            //Creando o consulta do BD-Banco de dados
            MySqlCommand coletar = new MySqlCommand(query, conexaoLogin);
            //Leitura do banco de dados
            MySqlDataReader leitura = coletar.ExecuteReader();
            c = 0;
            contadorLogin = 0;
            while (leitura.Read() == true)
            {
                codigousuario[c] = Convert.ToInt32(leitura["codigousuario"]);
                usuario[c] = leitura["usuario"] + "";
                senha[c] = leitura["senha"] + "";
                c++;
                contadorLogin++;
            }// fim do while
             //fecha a leitura do banco de dados
            leitura.Close();
        }//fim do metodo preencher vetor

        public string ConsultarLogin()
        {
            //preencher os vetores
            preencherVetor();
            msgLogin = "";

            for (c = 0; c < contadorLogin; c++)
            {
                msgLogin += "Código do Login:" + codigousuario[c] +
                ", usuario: " + usuario[c] +
                ", senha: " + senha[c] +
                "\n\n";
            }//fim do for
            return msgLogin;
        }//fim do metodo consultar login

        public string ConsultarLogin(int cod)
        {
            //preencher os vetores
            preencherVetor();
            msgLogin = "";
            for (c = 0; c < contadorLogin; c++)
            {
                if (codigousuario[c] == cod)
                {
                    msgLogin = "Código do Login:" + codigousuario[c] +
                    ", usuario: " + usuario[c] +
                    ", senha: " + senha[c] +
                    "\n\n";
                    return msgLogin;
                }
            }//fim do for
            return "Código informado não encontrado";
        }//fim do metodo consultar login

        public string AtualizarLogin(int codigo, string campoLogin, string novoDadolo)
        {
            try
            {
                string query = " Update usuario set " + campoLogin + " = '" + novoDadolo + "' Where codigousuario = '" + codigo + "'";
                //Exercutar o comando
                MySqlCommand sql = new MySqlCommand(query, conexaoLogin);
                string resultadoLogin = "" + sql.ExecuteNonQuery();
                //mostrar resultado em tela
                return resultadoLogin + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }//fim do catch
        }//fim do metodo atualizar login

        public string DeletarLogin(int cod)
        {
            try
            {
                string query = " Delete from usuario Where codigousuario = '" + cod + "'";
                MySqlCommand sql = new MySqlCommand(query, conexaoLogin);
                string resultadoLogin = "" + sql.ExecuteNonQuery();
                // mostrar o resultado em tela
                return resultadoLogin + "linha Alterada";
            }
            catch (Exception e)
            {
                return "Algo esta errado " + e;
            }

        }//fim do metodo Deletar usuario
        public string ValidarLogin(string user, string sen)
        {
            preencherVetor();
            msgValidacao = "";
            c++;
            contadorLogin++;
            for (c = 0; c < contadorLogin; c++)
            {
                if (usuario[c] == user && senha[c] == sen)
                {
                    return msgValidacao = "Bem vindo!";
                }
                else
                {
                    msgValidacao = "Senha ou usuario incorretos.";
                }
            }//fim do for
            return msgValidacao;
        }//fim do validar login
    }//fim da classe
}//fim do projeto