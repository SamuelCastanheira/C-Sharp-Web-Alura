﻿using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
namespace ScreenSound.Banco;

class Connection
{
    private string connectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound;Integrated Security=True;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public SqlConnection ObterConexao() {
        return new SqlConnection(connectionString);
    }

    public IEnumerable<Artista> Listar() {
        var lista = new List<Artista>();
        using var connection = ObterConexao();
        connection.Open();
        string sql = "SELECT * FROM Artistas";
        SqlCommand command = new SqlCommand(sql,connection);
        using SqlDataReader dataReader = command.ExecuteReader();
        while (dataReader.Read()) {
            string nomeArtista = Convert.ToString(dataReader["Nome"]);
            string bioArtista = Convert.ToString(dataReader["Bio"]);
            int idArtista = Convert.ToInt32(dataReader["Id"]);
            Artista artista = new Artista(nomeArtista, bioArtista)
            {
                Id = idArtista
            };
            lista.Add(artista);
        }
        return lista;
    }
}
