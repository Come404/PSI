namespace PSI1
{
    class Program
    {
        static void Open()
        {
            string data;
            int[,] dBTable = new int[2,78];
            string tempo = "a";
            string tempo2 = "b";
            string tempo3 = "9"; //freifor
            string autreCote = "";
            int panique = 0;
            int indice = 0;
            StreamReader sr = null;
            try
            {
                sr = new StreamReader("C:\\Users\\comew\\source\\repos\\PSI Project Perso\\PSI Project Perso\\soc-karate.mtx");
                data = sr.ReadLine();
                while (data != null)
                {
                    
                    if (data == null)
                        continue;
                    tempo = Convert.ToString(data[0]);
                    if (data[1] == ' ')
                        panique++;
                    else
                        tempo2 = Convert.ToString(data[1]);
                    if(tempo2 != "b")
                        tempo3 = tempo + tempo2;
                    else
                        tempo3 = tempo;

                    if (data[2] == ' ')
                        autreCote = Convert.ToString(data[3]);
                    else
                        autreCote = Convert.ToString(data[2]);
                    dBTable[0,indice] = Convert.ToInt32(tempo3);
                    dBTable[1,indice] = Convert.ToInt32(autreCote);
                    indice++;
                    data = sr.ReadLine();

                    tempo = "a";
                    tempo2 = "b";
                    tempo3 = "9";
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sr.Close();
            }
            AfficherMat(dBTable);
            Console.WriteLine(dBTable.GetLength(1));
        }
        static void AfficherMat(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j]+"; ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Open();
        }
    }
}