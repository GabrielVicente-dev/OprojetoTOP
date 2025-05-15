using MySql.Data.MySqlClient;
using OprojetoTop.Models;
using System.Data;


namespace OprojetoTop.Repositorio
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");
        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM Usuario WHERE Email = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Email = dr["Email"].ToString(),
                            Senha = dr["Senha"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }

        public void Cadastrar(Usuario cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Usuario (Nome,Email,Senha) values (@nome, @email, @senha)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cliente.Senha;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }





    }
}