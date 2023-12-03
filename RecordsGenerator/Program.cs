using System.Text;

Console.WriteLine("Podaj ile rekordów ma zostać utworzonych: ");
int sizeOfRecords = 5;
int MAXIMUM_NUMBER = 10000;
int MAXIMUM_GENERATED = 100000;
int howManyFiles = 100;
int numer = 1;
int option = 0;
bool parsed = false;
string path = AppDomain.CurrentDomain.BaseDirectory;
Random rand = new Random();

while (!parsed)
{
    string ile = Console.ReadLine();
    parsed = Int32.TryParse(ile, out numer);
    if (numer < 0 || numer > MAXIMUM_GENERATED) {
        parsed = false;
        Console.WriteLine("Nieodpowiednia liczba!");
    }
}
int whileLoop = numer;
path += numer.ToString();
System.IO.Directory.CreateDirectory(path);
Console.WriteLine("Wybierz sposób generowania rekordów:");
Console.WriteLine("1. Każdy kolejny rekord jest malejący");
Console.WriteLine("2. Każdy rekord jest wygenerowany losowo");
parsed = false;
while (!parsed)
{
    string ile = Console.ReadLine();
    parsed = Int32.TryParse(ile, out option);
    if (option < 0 || option > MAXIMUM_NUMBER) {
        parsed = false;
        Console.WriteLine("Wybierz poprawną opcję!");
    }
}

for (int k = 0; k < howManyFiles; k++)
{
    using (var stream = System.IO.File.Open(path + Path.DirectorySeparatorChar + k.ToString() + ".txt", FileMode.OpenOrCreate))
    {
        using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
        {
            numer = whileLoop;
            while (numer > 0)
            {
                double num = (double)numer;
                for (int i = 0; i < sizeOfRecords; i++)
                {
                    if (option == 1)
                    {
                        writer.Write(num);
                    }
                    else if (option == 2)
                    {
                        writer.Write(rand.NextDouble() * MAXIMUM_NUMBER);
                    }
                }
                numer--;
            }
        }
    }
}