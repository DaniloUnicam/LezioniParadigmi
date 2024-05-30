using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Abstractions;

namespace Unicam.Paradigmi.Test.Examples
{
    public class AdoNetExample : IExample
    {
        public void RunExample()
        {
            using (var connection = new SqlConnection()) { 
                connection.ConnectionString = "Server=PCDANI;Database=Paradigmi;User Id=paradigmi;Password=paradigmi;";
                connection.Open();
                var cmd = new SqlCommand();
                cmd.Connection = connection;
                //per evitare sqlinjection, è buon uso
                //utilizzare la chiocciola seguito dal nome della tabella su sql
                //come @CITTA invece di 'Tolentino'.
                cmd.CommandText = "INSERT INTO Aziende(RagioneSociale," +
                    "Citta,Cap) VALUES('@RAGIONE_SOCIALE','@CITTA','@CAP')";
                //esegue il query senza ritornare valori
                cmd.ExecuteNonQuery();
                connection.Close();
        }
    }
    }
}
