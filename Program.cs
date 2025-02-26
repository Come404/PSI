using PSI_Project_Perso;

namespace PSI1
{
    class Program
    {
        static int[,] Open()
        {
            string data;
            int[,] dBTable = new int[2, 78]; // [0,...] : le noeud || [1,...] : le noeud relié à celui-ci
            int indice = 0;
            StreamReader sr = null;
            try
            {
                //standard pour lire un fichier ligne par ligne; ici les chiffres commencent à la 1ere ligne
                sr = new StreamReader("C:/Users/comew/OneDrive/Documents/GitHub/PSI/soc-karate.mtx");
                data = sr.ReadLine();
                while (data != null)
                {
                    if (data == null)
                        continue;

                    int[] numbers = data.Split(' ').Select(int.Parse).ToArray();
                    dBTable[0, indice] = numbers[0]; // First number in the first row
                    dBTable[1, indice] = numbers[1]; // Second number in the second row
                    indice++;
                    data = sr.ReadLine();
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
            return dBTable;
        }
        static void AfficherMat(int[,] matrix)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[0, j] + " ");
                Console.Write(matrix[1, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Ajuster(int[,] matrice)
        {
            Noeud[] joueurs = new Noeud[34];
            for (int j = 0; j < matrice.GetLength(0); j++)
            {
                joueurs[j] = new Noeud(matrice[0, j]);
                joueurs[j].Rajouter(matrice[1, j]);
                Console.WriteLine(joueurs[j].Voisins[0]);
            }
        }
        /* //Pour compter le nombre d'occurences, pour l'instant pas d'utilité
        static int NbOccurences(int[,] matrix)
        {
            int occ = 0;
            int[] tab = new int[78];
            for (int i = 0; i< matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[0, j] == matrix[0,i])
                        tab[i] = tab[i]+1;
                }
            }
            return occ;
        }*/
        static void Main(string[] args)
        {
            Ajuster(Open());
        }
    }
}