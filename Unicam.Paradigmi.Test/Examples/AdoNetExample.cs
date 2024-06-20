using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Test.Examples
{
    public class AdoNetExample : IExample
    {
        public async Task RunExampleAsync()
        {

        }
        public void RunExample()
        {
            GetAziende();
        }        

        private List<Azienda> GetAziende()
        {
            List<Azienda> list = new List<Azienda>();
            {
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = "Server=PCDANI;Database=Paradigmi;User Id=paradigmi;Password=paradigmi;";
                    connection.Open();
                    var cmd = new SqlCommand();
                    cmd.Connection = connection;
                    //Non si usa l'asterisco * per selezionare tutto
                    cmd.CommandText = "SELECT IdAzienda,RagioneSociale,Citta,Cap FROM Aziende";
                    //Siccome DataReader implementa IDisposable, bisogna
                    //inserirlo all'interno di uno using.
                    using (var reader = cmd.ExecuteReader())
                        if (reader.HasRows)
                        {
                            //il reader andrà a leggere sugli index
                            //reader è un oggetto generico, quindi l'indexer
                            //restituirà sempre un object
                            //quindi è opportuno creare una classe di oggetti Azienda
                            //per cicliare
                            while (reader.Read())
                            {
                                var azienda = new Azienda();
                                azienda.IdAzienda = (int)reader["IdAzienda"];
                                azienda.RagioneSociale = (string)reader["RagioneSociale"];
                                azienda.Citta = (string)reader["Citta"];
                                azienda.Cap = (string)reader["Cap"];
                                list.Add(azienda);
                            }
                        }
                }
            }
            return list;
        }

        private void AddAzienda()
        {
            {
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = "Server=PCDANI;Database=Paradigmi;User Id=paradigmi;Password=paradigmi;";
                    connection.Open();
                    var cmd = new SqlCommand();
                    cmd.Connection = connection;
                    //per evitare sqlinjection, è buon uso
                    //utilizzare la chiocciola seguito dal nome della tabella su sql
                    //come @CITTA invece di 'Tolentino'
                    //@ sarebbe un placeholder
                    cmd.CommandText = "INSERT INTO Aziende(RagioneSociale,Citta,Cap) " +
                        "VALUES(@RAGIONE_SOCIALE,@CITTA,@CAP)";
                    //esegue il query senza ritornare valori
                    cmd.Parameters.AddWithValue("@RAGIONE_SOCIALE", "UNICAM");
                    cmd.Parameters.AddWithValue("@CITTA", "Camerino");
                    cmd.Parameters.AddWithValue("@CAP", "98765");

                    cmd.ExecuteNonQuery();
                }
            }
    }
    }
}
