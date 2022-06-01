using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Livraria2
{
    class DaoReserva
    {
        public MySqlConnection conexaoReserva;
        public string dados;
        public string comando;
        public string resultado;
        public int s;
        public string msgReserva;
        public int contadorReserva;
        public int[] codigoReserva;//Vetor de código
        public int[] quantidadeReserva;//Vetor de nome
        public DateTime[] dataDaReserva;//Vetor de datas

        public DaoReserva()
        {
            conexaoReserva = new MySqlConnection(" server=localhost;DataBase=Livraria2;Uid=root;password=;Convert Zero DateTime=True ");
            try
            {
                conexaoReserva.Open();// tentando a conexão com banco de dados
                Console.WriteLine(" Conectado com sucesso ");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(" algo deu errado\n\n " + e);// mostra o erro em tela
                conexaoReserva.Close();// fecha a conexão com banco de dados
                Console.ReadLine();
            }

        }//fim do contrutor
        public void InserirReserva(string reserva, int quantidade, DateTime dataDaReserva)
        {
            try
            {
                //Modificar a estrutura de data
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@Date";
                parameter.MySqlDbType = MySqlDbType.Date;
                parameter.Value = dataDaReserva.Year + "-" + dataDaReserva.Month + "-" + dataDaReserva.Day;
                //Preparo o código para inserção no banco
                dados = "('','" + quantidade + "','" + parameter.Value + "')";
                comando = "Insert into Reserva(codigoReserva, dataDaReserva, quantidade) values" + dados;
                //Executar o comando de inserção no banco de dados
                MySqlCommand sql = new MySqlCommand(comando, conexaoReserva);
                resultado = "" + sql.ExecuteNonQuery();//Executa o insert no BD
                Console.WriteLine(resultado + " Linhas Afetadas");
            }
            catch (Exception e)
            {
                Console.WriteLine("Algo deu errado!\n\n" + e);
                Console.ReadLine();//Manter o programa aberto
            }
        }//fim do inserir

        public void PreencherVetor()
        {
            string query = "select * from Reserva ";//Coletar os dados do BD

            //Instanciar
            codigoReserva = new int[100];
            quantidadeReserva = new int[100];
            dataDaReserva = new DateTime[100];

            //Preencher com valores iniciais
            for (s = 0; s < 100; s++)
            {
                codigoReserva[s] = 0;
                quantidadeReserva[s] = 0;
                dataDaReserva[s] = new DateTime();
            }//fim do for

            //Criando o comando para consultar no BD
            MySqlCommand coletar = new MySqlCommand(query, conexaoReserva);
            //Leitura dos dados do banco
            MySqlDataReader leitura = coletar.ExecuteReader();
            s = 0;
            contadorReserva = 0;
            while (leitura.Read())
            {
                codigoReserva[s] = Convert.ToInt32(leitura["codigoReserva"]);
                quantidadeReserva[s] = Convert.ToInt32(leitura["quatidade"]);
                dataDaReserva[s] = Convert.ToDateTime(leitura["dataDaReserva"]);
                s++;
                contadorReserva++;
            }//Fim do while

            //Fechar a leitura de dados no banco
            leitura.Close();
        }//fim do método de preenchimento do vetor

        //Método que consulta TODOS OS DADOS no banco de dados
        public string ConsultarReserva()
        {
            //Preencher os vetores
            PreencherVetor();
            msgReserva = "";
            for (s = 0; s < contadorReserva; s++)
            {
                msgReserva += "Código da Reserva: " + codigoReserva[s] +
                ", quantidade: " + quantidadeReserva[s] +
                ", Data de Reserva: " + dataDaReserva[s] +
                "\n\n";
            }//fim do for
            return msgReserva;
        }//fim do método consultar

        public string ConsultarReserva(int cod)
        {
            PreencherVetor();
            for (s = 0; s < contadorReserva; s++)
            {
                if (codigoReserva[s] == cod)
                {
                    msgReserva = "Código: " + codigoReserva[s] +
                    ", quantidade: " + quantidadeReserva[s] +
                    ", Data de Reserva: " + dataDaReserva[s] +
                    "\n\n";
                    return msgReserva;
                }
            }//fim do for
            return "Código informado não encontrado!";
        }//fim do consultar

        public string AtualizarReserva(int codigo, string campo, string novoDado)
        {
            try
            {
                string query = "update Reserva set " + campo + " = '" + novoDado + "' where codigoReserva = '" + codigo + "'";
                //Executar o comando
                MySqlCommand sql = new MySqlCommand(query, conexaoReserva);
                string resultadoReserva = "" + sql.ExecuteNonQuery();
                return resultadoReserva + "Linha Afetada";
            }
            catch (Exception e)
            {
                return "Algo deu errado!\n\n" + e;
            }
        }//fim do método Atualizar

        public string Deletar(int codigo)
        {
            try
            {
                string query = "delete from Reserva where codigoReserva = '" + codigo + "'";
                //Preparar o comando
                MySqlCommand sql = new MySqlCommand(query, conexaoReserva);
                string resultadoReserva = "" + sql.ExecuteNonQuery();
                //Mostrar a mensagem em tela
                return resultadoReserva + "Linha Afetada";
            }
            catch (Exception e)
            {
                return "Algo deu errado!\n\n" + e;
            }
        }//fim do deletar
    }//fim da classe
}//fim da projeto