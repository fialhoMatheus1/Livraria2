using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Livraria2
{
    class DAOCompra
    {
        public MySqlConnection conexaoCompra;
        public string dados;
        public string comando;
        public string resultado;
        public DateTime[] dataDaCompra;
        public int[] quantidade;
        public double[] valorTotal;
        public int[] cvv;
        public int[] numero;
        public int[] nomeTitular;
        public int[] codigoCompra;
        public int b;
        public int contadorCompra;
        public string msgCompra;



        public DAOCompra()
        {
            conexaoCompra = new MySqlConnection(" server=localhost;DataBase=Livraria2;Uid=root;password=;Convert Zero DateTime=True ");
            try
            {
                conexaoCompra.Open();// tentando a conexão com banco de dados 
                Console.WriteLine("  Conectado com sucesso  ");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                conexaoCompra.Close();// fecha a conexão com banco de dados
                Console.ReadLine();
            }
        }//fim do metodo construtor

        public void InserirCompra(string quantidade, string valorTotal, DateTime dataDaCompra, string cvv, string numero, string nomeTitular)

        {

            try
            { // modificar a  estrutura de dados

                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = " @Date ";
                parameter.MySqlDbType = MySqlDbType.Date;
                parameter.Value = dataDaCompra.Year + "-" + dataDaCompra.Month + "-" + dataDaCompra.Day;
                dados = "('','" + quantidade + "','" + valorTotal + "','" + parameter.Value + "','" + cvv + "','" + nomeTitular + "','" + numero + "')";

                comando = " Insert into Compra(codigoCompra, quantidade, valorTotal, dataDaCompra) values " + dados;
                // Exercutar o comando de inserção banco de dados

                MySqlCommand sql = new MySqlCommand(comando, conexaoCompra);
                resultado = " " + sql.ExecuteNonQuery();// Executa o insert no BD
                Console.WriteLine(resultado + " Linhas Afetadas ");
            }

            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                Console.ReadLine();// manter o prompt aberto

            }// fim do metodo construtor

        }

        public void preencherVetor()
        {
            string query = " select * from Compra ";
            // instanciar
            codigoCompra = new int[100];
            quantidade = new int[100];
            valorTotal = new double[100];
            dataDaCompra = new DateTime[100];
            cvv = new int[100];
            numero = new int[100];
            nomeTitular = new int[100];

            //preencher com valores inciciais 
            for (b = 0; b < 100; b++)
            {
                codigoCompra[b] = 0;
                quantidade[b] = 0;
                valorTotal[b] = 0;
                dataDaCompra[b] = new DateTime();
                cvv[b] = 0;
                numero[b] = 0;
                nomeTitular[b] = 0;
            }//fim do for

            //Creando o consulta do BD-Banco de dados 
            MySqlCommand coletar = new MySqlCommand(query, conexaoCompra);
            //Leitura do banco de dados
            MySqlDataReader leitura = coletar.ExecuteReader();
            b = 0;
            contadorCompra = 0;
            while (leitura.Read() == true)

            {
                codigoCompra[b] = Convert.ToInt32(leitura["codigoCompra"]);
                quantidade[b] = Convert.ToInt32(leitura["quantidade"]);
                valorTotal[b] = Convert.ToDouble(leitura["valortotal"]);
                dataDaCompra[b] = Convert.ToDateTime(leitura["dataDaCompra"]);
                cvv[b] = Convert.ToInt32(leitura["cvv"]);
                numero[b] = Convert.ToInt32(leitura["numero"]);
                nomeTitular[b] = Convert.ToInt32(leitura["nomeTitular"]);
                b++;
                contadorCompra++;
            }// fim do while

            //fecha a leitura do banco de dados
            leitura.Close();

        }
        public string ConsultarCompra()
        {
            //preencher os vetores
            preencherVetor();

            msgCompra = "";

            for (b = 0; b < contadorCompra; b++)
            {
                msgCompra += "Código da Compra:" + codigoCompra[b] +
                       ", quantidade: " + quantidade[b] +
                       ", valor Total: " + valorTotal[b] +
                       ", Data da Compra: " + dataDaCompra[b] +
                       ", cvv: " + cvv[b] +
                       ", numero: " + numero[b] +
                       ", nomeTitular: " + nomeTitular[b] +
                       "\n\n";
            }//fim do for
            return msgCompra;
        }//fim do metodo consultar livro
        public string ConsultarCompra(int cod)
        {
            //preencher os vetores
            preencherVetor();

            msgCompra = "";

            for (b = 0; b < contadorCompra; b++)
            {
                if (codigoCompra[b] == cod)
                {
                    msgCompra = "Código do Compra:" + codigoCompra[b] +
                       ", quantidade: " + quantidade[b] +
                       ", valor Total: " + valorTotal[b] +
                       ", Data da Compra: " + dataDaCompra[b] +
                       ", cvv: " + cvv[b] +
                       ", numero: " + numero[b] +
                       ", nomeTitular: " + nomeTitular[b] +
                       "\n\n";
                    return msgCompra;
                }
            }//fim do for
            return "Código informado não encontrado";
        }//fim do metodo consultar livro

        public string AtualizarCompra(int cod, string campo, string novoDado)
        {
            try
            {
                string query = " Update Compra set " + campo + " = '" + novoDado + "' Where codigoCompra = '" + cod + "'";
                //Exercutar o comando
                MySqlCommand sql = new MySqlCommand(query, conexaoCompra);
                string resultado = "" + sql.ExecuteNonQuery();

                return resultado + "linha Alterada";

            }
            catch (Exception e)
            {

                return "Algo esta errado " + e;

            }//fim do catch
        }//fim do metodo atualizar compra
    }//fim da classe
}//fim do projeto