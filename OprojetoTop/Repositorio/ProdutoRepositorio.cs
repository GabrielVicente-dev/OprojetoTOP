using MySql.Data.MySqlClient;
using OprojetoTop.Models;
using OprojetoTop.Models;
using System.Configuration;
using System.Data;

namespace ProjetoEcommerce.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");


        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Produto (Nome, Descricao, quantidade, preco) values (@nome, @descricao, @quantidade, @preco)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                cmd.Parameters.Add("@preco", MySqlDbType.Float).Value = produto.preco;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }




        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Produto where CodProd=@CodProd", conexao);
                cmd.Parameters.AddWithValue("@CodProd", Id);
                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> Produtolist = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Produtolist.Add(
                                new Produto
                                {
                                    CodProd = Convert.ToInt32(dr["CodProd"]),
                                    Nome = ((string)dr["Nome"]),
                                    Descricao = ((string)dr["Descricao"]),
                                    quantidade = ((int)dr["quantidade"]),
                                    preco = ((decimal)dr["preco"]),
                                });
                }
                return Produtolist;
            }
        }

        public Produto ObterProduto(int Codigo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produto where CodProd=@CodProd ", conexao);
                cmd.Parameters.AddWithValue("@CodProd", Codigo);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.CodProd = Convert.ToInt32(dr["CodProd"]);
                    produto.Nome = (string)(dr["Nome"]);
                    produto.Descricao = (string)(dr["Descricao"]);
                    produto.quantidade = (int)(dr["quantidade"]);
                    produto.preco = (decimal)(dr["preco"]);
                }
                return produto;
            }
        }


        public void AtualizarProduto(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("update Produto set Nome=@nome, Descricao=@descricao, quantidade=@quantidade, preco=@preco where CodProd=@CodProd;", conexao);
                    cmd.Parameters.Add("@CodProd", MySqlDbType.Int32).Value = produto.CodProd;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                    cmd.Parameters.Add("@preco", MySqlDbType.Float).Value = produto.preco;
                    cmd.ExecuteNonQuery();
                    conexao.Close();

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar o Produto: {ex.Message}");

            }
        }

        public IEnumerable<Produto> TodosProduto()
        {
            List<Produto> Produtolist = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Produtolist.Add(
                                new Produto
                                {
                                    CodProd = Convert.ToInt32(dr["CodProd"]),
                                    Nome = ((string)dr["Nome"]),
                                    Descricao = ((string)dr["Descricao"]),
                                    quantidade = ((int)dr["quantidade"]),
                                    preco = ((decimal)dr["preco"]),
                                });
                }
                return Produtolist;
            }
        }

    }

}
