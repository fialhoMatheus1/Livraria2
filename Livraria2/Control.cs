using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria2
{
    class Control
    {
        //instanciar a classe DAO
        DAO conexao;// criando a variavel conexao
        DAOLivro conexaoLivro;
        DAOCompra conexaoCompra;
        DAOLogin conexaoLogin;
        DAOAutor conexaoAutor;
        DAOCategoria conexaoCategoria;
        DaoReserva conexaoReserva;
        public int opcao;
        public DateTime datadenascimento;
        public DateTime dataDePublicacao;
        public DateTime dataDaCompra;
        public Control()
        {
            conexao = new DAO();//instaciando as variaveis de conexao
            conexaoLivro = new DAOLivro();
            conexaoCompra = new DAOCompra();
            conexaoLogin = new DAOLogin();
            conexaoAutor = new DAOAutor();
            conexaoCategoria = new DAOCategoria();
            conexaoReserva = new DaoReserva();
            datadenascimento = new DateTime();//instanciando variavel de data
            dataDaCompra = new DateTime();
        }// fim do contrutor

        public void Menu()
        {
            Console.WriteLine(" Escolhar uma das opçoes abaixo:\n\n" +

                                "1. Cadastrar\n" +
                                "2. Consultar todos os clientes\n" +
                                "3. Consultar cliente por código\n" +
                                "4. Atualizar dados de Cliente Login\n" +
                                "5. Deletar Cliente\n" +
                                "6. Login\n" +
                                "7. Consultar Login\n" + 
                                "8. Atualizar Login\n" +
                                "9. Deletar Login\n" +
                                "10. Cadastrar Autor\n" +
                                "11. Consultar Autor\n" +
                                "12. Atualizar Autor\n" +
                                "13. Deletar Autor\n" +
                                "14. Cadastrar livro\n" +
                                "15. Consultar todos os livro\n" +
                                "16. Consultar livro por Código\n" +
                                "17. Atualizar dados de livros\n" +
                                "18. Deletar livro\n" +
                                "19. Comprar\n"+
                                "19. Comprar\n" +
                                "19. Comprar\n" +
                                "19. Comprar\n" +
                                ".cadastrar Reservar\n" +
                                ". Consultar Reservar\n" +
                                ". Atualizar Reservar\n" +
                                ". Deletar Reservar\n" +
                                "0. sair ");
            opcao = Convert.ToInt32(Console.ReadLine());

        }//Fim do Menu

        public void Executar()
        {
            
            do
            {
                Menu();
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Obrigado!");
                        break;
                    case 1:
                        Console.WriteLine(" Informe o seu Nome: ");
                        string nome = Console.ReadLine();
                        Console.WriteLine(" Informe o seu Telefone: ");
                        string telefone = Console.ReadLine();
                        Console.WriteLine(" Informe o seu Endereço: ");
                        string endereco = Console.ReadLine();
                        Console.WriteLine(" Informe o sua data de nascimento: ");
                        datadenascimento = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine(" Digite um usuario: ");
                        string usuario = Console.ReadLine();
                        Console.WriteLine(" Digite uma senha: ");
                        string senha = Console.ReadLine();
                        conexao.Inserir(nome, telefone, endereco, datadenascimento, usuario, senha);
                        break;
                    case 2:
                        Console.WriteLine(conexao.ConsultarCliente());
                        break;
                    case 3:
                        Console.WriteLine("Informe o código do cliente que deseja consultar: ");
                        int codigo = Convert.ToInt32(Console.ReadLine());
                        //mostrar resultado em tela
                        Console.WriteLine(conexao.ConsultarCliente(codigo));
                        break;
                    case 4:
                        //Solicitar o campo que sera alterado
                        Console.WriteLine(" Informe o campo que deseja altera: ");
                        string campo = Console.ReadLine();
                        Console.WriteLine(" Informe o novo dado para esse campo: ");
                        string novoDado = Console.ReadLine();
                        Console.WriteLine(" Informe o codigo do cliente que deseja alterar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //ultilizar os dados acima no atualizar
                        Console.WriteLine(conexao.AtualizarCliente(codigo, campo, novoDado));
                        break;
                    case 5:
                        //solicitar o codigo que sera apagado
                        Console.WriteLine(" Informe o codigo do cliente que deseja Deletar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        // mostrar em tela
                        Console.WriteLine(conexao.DeletarCliente(codigo));
                        break;
                    case 6:
                        Console.WriteLine("Digite seu usuário: ");
                        string user = Console.ReadLine();
                        Console.WriteLine("Digite sua senha: ");
                        string sen = Console.ReadLine();
                        Console.WriteLine(conexaoLogin.ValidarLogin(user, sen));
                        break;
                    case 7:
                        Console.WriteLine(" Informe o codigo do login que deseja consultar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(conexaoLogin.ConsultarLogin(codigo));
                        break;
                    case 8:
                        //Solicitar o campo que sera alterado
                        Console.WriteLine(" Informe o campo que deseja altera: ");
                        string campoLogin = Console.ReadLine();
                        Console.WriteLine(" Informe o novo dado para esse campo: ");
                        string novoDadolo = Console.ReadLine();
                        Console.WriteLine(" Informe o codigo do usuario que deseja alterar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //ultilizar os dados acima no atualizar
                        Console.WriteLine(conexaoLogin.AtualizarLogin(codigo, campoLogin, novoDadolo));
                        break;
                    case 9:
                        //solicitar o codigo que sera apagado
                        Console.WriteLine(" Informe o codigo do livro que deseja Deletar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        // mostrar em tela
                        Console.WriteLine(conexaoLogin.DeletarLogin(codigo));
                        break;
                    case 10:
                        //Cadastra Autor
                        Console.WriteLine(" Informe o Nome do Autor: ");
                        string nomeAutor = Console.ReadLine();
                        conexaoAutor.InserirAutor(nomeAutor);
                        break;
                    case 11:
                        //Coletando o código que será pesquisado
                        Console.WriteLine("Informe o código do autor que deseja consultar: ");
                        int cod = Convert.ToInt32(Console.ReadLine());
                        //Mostrando o resultado em tela
                        Console.WriteLine(conexaoAutor.consultarAutor(cod));
                        break;
                    case 12:
                        //Solicitar os campos que serão atualizados
                        Console.WriteLine("Informe o campo que deseja atualizar: ");
                        string campoAo = Console.ReadLine();
                        Console.WriteLine("Informe o novo dado para esse campo: ");
                        string novoDadoAo = Console.ReadLine();
                        Console.WriteLine("Informe o código do autor que deseja atualizar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //utilizar os dados acima no método atualizar
                        Console.WriteLine(conexaoAutor.AtualizarAutor(codigo, campoAo, novoDadoAo));
                        break;
                    case 13:
                        //Solicitar o código que será apagado
                        Console.WriteLine("Informe o código que deseja apagar");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //Mostrar o resultado em tela
                        Console.WriteLine(conexaoAutor.DeletarAutor(codigo));
                        break;
                    case 14:
                        Console.WriteLine(" Informe o seu Titulo: ");
                        string titulo    = Console.ReadLine();
                        Console.WriteLine(" Informe o Nome do Autor: ");
                        string nomeDoAutor = Console.ReadLine();
                        Console.WriteLine(" Informe a data de publicação: ");
                        dataDePublicacao = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine(" Informe o nome da editora: ");
                        string editora = Console.ReadLine();
                        Console.WriteLine(" Informe a categoria: ");
                        string categoria = Console.ReadLine();
                        conexaoLivro.InserirLivro(titulo, nomeDoAutor, dataDePublicacao, editora, categoria);
                        break;
                    case 15:
                        Console.WriteLine(conexaoLivro.ConsultarLivros());
                        break;
                    case 16:
                        Console.WriteLine("Informe o código do livro que deseja consultar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //mostrar resultado em tela
                        Console.WriteLine(conexaoLivro.ConsultarLivros(codigo));
                        break;
                    case 17:
                        //Solicitar o campo que sera alterado
                        Console.WriteLine(" Informe o campo que deseja altera: ");
                        campo = Console.ReadLine();
                        Console.WriteLine(" Informe o novo dado para esse campo: ");
                        novoDado = Console.ReadLine();
                        Console.WriteLine(" Informe o codigo do usuario que deseja alterar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //ultilizar os dados acima no atualizar
                        Console.WriteLine(conexaoLivro.AtualizarLivros(codigo, campo, novoDado));
                        break;
                    case 18:
                        //solicitar o codigo que sera apagado
                        Console.WriteLine(" Informe o codigo do livro que deseja Deletar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        // mostrar em tela
                        Console.WriteLine(conexaoLivro.DeletarLivro(codigo));
                        break;
                    case 19:
                        Console.WriteLine("Digite o numero do seu cartão:");
                        int numero = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Digite o cvv:");
                        int cvv = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Digite o nome do titular:");
                        string nomeTitular =Console.ReadLine();
                        Console.WriteLine("Digite a quantidade que deseja comprar:");
                        int quantidade = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Digite a quantidade a ser paga:");
                        double valorTotal = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Digite a data da compra:");
                        dataDaCompra = Convert.ToDateTime(Console.ReadLine());
                        break;


                    case :

                        //Coletando o código que será pesquisado
                        Console.WriteLine("Informe o código da reserva que deseja consultar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //Mostrando o resultado em tela
                        Console.WriteLine(conexaoReserva.ConsultarReserva(codigo));
                        break;

                    default:
                        Console.WriteLine(" Codigo Inserido não é valido ");
                        break;


                }// fim do switch

            } while (opcao != 0);
        }




    }//Fim da classe
}// Fim do projeto
