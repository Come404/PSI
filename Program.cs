using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PSI_Project_Perso;

namespace PSI_Project_Perso
{
    class Program
    {
        static void Main()
        {
            // obtenir les donnees de graphe et creation deu graphe
            List<(int, int)> edges = ReadEdgesFromFile("soc-karate.mtx");
            Graphe graph = new Graphe(edges);

            Console.WriteLine("\nOrdre de graphe (le nombre de sommets qu'il contient) =  " + graph.OrdreDeGraphe());
            Console.WriteLine("\nTaille de graphe (le nombre d'arêtes du graphe) =  " + graph.TailleDeGraphe());

            // imprimer la matrice d'adjacence
            graph.AfficherMatrice();
            Console.WriteLine("\nGraph est non-orienté ? " + (graph.EstNonOriente() ? "Oui" : "Non"));

            // executer le DFS et avoir le resultat
            Console.WriteLine("\nExécution de DFS:");
            graph.DFS_Main();
            Console.WriteLine(graph.ContientCycle ? "Le graphe contient un cycle." : "Le graphe ne contient pas de cycle.");

            // executer le BFS, obtenir l'ordre de visite
            Console.WriteLine("\nExécution de BFS depuis le sommet 1:");
            List<int> bfsOrder = graph.BFS(1);
            graph.AfficherBFSOrder(bfsOrder);
        }

        /// <summary>
        ///  obtenir les donnees de graphe et obtenir le liste des arcs
        /// </summary>
        static List<(int, int)> ReadEdgesFromFile(string filePath)
        {
            List<(int, int)> edges = new List<(int, int)>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string data;
                while ((data = sr.ReadLine()) != null)
                {
                    int[] numbers = data.Split(' ').Select(int.Parse).ToArray();
                    edges.Add((numbers[0], numbers[1]));
                }
            }
            return edges;
        }
    }
}