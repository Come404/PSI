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
            // 读取图数据并创建图
            List<(int, int)> edges = ReadEdgesFromFile("soc-karate.mtx");
            Graphe graph = new Graphe(edges);

            Console.WriteLine("\nOrdre de graphe (le nombre de sommets qu'il contient) =  " + graph.OrdreDeGraphe());
            Console.WriteLine("\nTaille de graphe (le nombre d'arêtes du graphe) =  " + graph.TailleDeGraphe());

            // 打印邻接矩阵
            graph.PrintAdjMatrix();
            Console.WriteLine("\nGraph est non-orienté ? " + (graph.EstNonOriente() ? "Oui" : "Non"));

            // 执行 DFS 并输出结果
            Console.WriteLine("\nExécution de DFS:");
            graph.DFS_Main();
            Console.WriteLine(graph.ContientCycle ? "Le graphe contient un cycle." : "Le graphe ne contient pas de cycle.");

            // 执行 BFS 并输出访问顺序
            Console.WriteLine("\nExécution de BFS depuis le sommet 1:");
            List<int> bfsOrder = graph.BFS(1);
            graph.PrintBFSOrder(bfsOrder);
        }

        /// <summary>
        /// 读取数据文件并转换为边的列表
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