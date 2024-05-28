using System.Text;
using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Test.Exceptions;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Test.Examples
{
    public class FileManagmentExample : IExample
    {
        public void RunExample()
        {
            string path = "Content\\alunni.csv";
            var listaAlunni = ReadAlunniFromCsv(path);
            //var listaAlunni = ReadAlunniCsvFromStream(path);
            var enumerableAlunni = ReadAlunniWithYield(path);
            //con enumerable, analizzo il testo appena ciclato e una volta finito,
            //proseguo col ciclare il file.
            foreach(var alunno in enumerableAlunni)
            {
                Console.WriteLine(alunno); 
            }

            listaAlunni.Add(new Alunno()
            {
                Cognome = "Aggiunto",
                Nome = "Alunno",
                Matricola = "XXXXX",
                DataNascita = new DateTime(2000, 10, 10)
            });

            SaveAlunniInCsv(path, listaAlunni);
            ReadAlunniCsvFromStream(path);

        }

        private void SaveAlunniInCsv(string path, List<Alunno> alunni)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Nome;Cognome;Matricola;DataNascita");
            foreach (var alunno in alunni)
            {
                sb.AppendLine($"{alunno.Nome};{alunno.Cognome};{alunno.Matricola};{alunno.DataNascita:dd//MM/yyyy}");
            }
            System.IO.File.WriteAllText(path, sb.ToString());

        }

        //yield serve a restituire man mano ciò che andiamo a leggere
        //serve ad evitare di fare un loop aggiuntivo per ottenere un oggetto enumerabile
        //invece di looppare 2 milioni di volte, looppiamo solo la prima volta
        private IEnumerable<Alunno> ReadAlunniWithYield(string path)
        {
            
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    int i = 0;
                    while(true)
                    {
                        string line = reader.ReadLine();
                        if(line == null)
                        {
                            break;
                        }
                        if(i>0)
                        {
                            //restituisce un oggetto IEnumerable
                            yield return new Alunno(line);
                        }
                        i++;
                    }
                }
            }
        }

        private List<Alunno> ReadAlunniCsvFromStream(string path)
        {
            List<Alunno> list = new List<Alunno>();
            //leggiamo pezzo pezzo il file
            //usiamo filestream dando una modalità del file, open lo apre
            //lo using distrugge la classe non appena finito il lavoro
            //lo handle apre il file, ma una volta finito il lavoro, lo dobbiamo chiudere
            //stream implementa l'interfaccia idisposable, che avrà un metodo dispose che chiuderà
            //automaticamente il file una volta terminato l'utilizzo.
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                int i = 0;
                //imposto lo stream da leggere (del file)
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (i > 0) { 
                        list.Add(new Alunno(line));
                        }
                        i++;
                    }
                }
                return list;
            }
        }


        private List<Alunno> ReadAlunniFromCsv(string path)
        {
            //Leggo il file alunni.csv
            //Path assoluto
            //System.IO.File.ReadAllText("C:\\Users\\Utente\\source\\repos\\Unicam.Paradigmi\\Unicam.Paradigmi.Test\\Content\\alunni.csv");
            //Path relativo
            //string contentAlunni = System.IO.File.ReadAllText("Content\\alunni.csv");
            var list = new List<Alunno>();
            if (File.Exists(path))
            {
                string[] righeAlunni = System.IO.File.ReadAllLines(path);
                int i = 0;
                foreach (var riga in righeAlunni)
                {
                    if (i > 0)
                    {
                        list.Add(new Alunno(riga));
                    }
                    i++;
                }
            }
            else
            {
                throw new FileErrorException("Impossibile aprire il path", path);
            }
            return list;


        }
    }
}
