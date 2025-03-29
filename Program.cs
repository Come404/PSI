using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PSI_Project_Perso
{
    class Program
    {
        static void Main()
        {
            // Lecture des données du graphe à partir d'un fichier et création du graphed
            List<(int, int)> edges = ReadEdgesFromFile("soc-karate.mtx");
            Graphe graph = new Graphe(edges);
            // Affichage de l'ordre et de la taille du graphe
            Console.WriteLine("\nOrdre de graphe (le nombre de sommets qu'il contient) =  " + graph.OrdreDeGraphe());
            Console.WriteLine("\nTaille de graphe (le nombre d'arêtes du graphe) =  " + graph.TailleDeGraphe());

            // Affichage de la matrice d'adjacence du graphe 
            graph.PrintAdjMatrix();
            Console.WriteLine("\nGraph est non-orienté ? " + (graph.EstNonOriente() ? "Oui" : "Non"));

            // Exécution de l'algorithme de parcours en profondeur (DFS)
            Console.WriteLine("\nExécution de DFS:");
            graph.DFS_Main();
            Console.WriteLine(graph.ContientCycle ? "Le graphe contient un cycle." : "Le graphe ne contient pas de cycle.");

            // Exécution de l'algorithme de parcours en largeur (BFS) depuis le sommet 1
            Console.WriteLine("\nExécution de BFS depuis le sommet 1:");
            List<int> bfsOrder = graph.BFS(1);
            graph.PrintBFSOrder(bfsOrder);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new GraphForm(graph));
        }

        /// <summary>
        /// Lit un fichier contenant les arêtes du graphe et retourne une liste de paires de sommets.
        /// </summary>
        /// <param name="filePath">Chemin du fichier contenant les données du graphe.</param>
        /// <returns>Liste des arêtes du graphe.</returns>
        static List<(int, int)> ReadEdgesFromFile(string filePath)
        {
            List<(int, int)> edges = new List<(int, int)>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string data;
#pragma warning disable CS8600 // Désactivation de l'avertissement de conversion de null
                while ((data = sr.ReadLine()) != null)
                {
                    int[] numbers = data.Split(' ').Select(int.Parse).ToArray();
                    edges.Add((numbers[0], numbers[1]));
                }
#pragma warning restore CS8600 // Réactivation de l'avertissement de conversion de null
            }
            return edges;
        }
    }
}
