using System;
using System.Collections.Generic;
using System.Linq;

class Graphe
{
    private int[,] adjMatrix;
    private int nbNoeuds;

    // 用于 DFS 的数据
    private Dictionary<int, string> couleurDFS;
    private Dictionary<int, int?> predDFS;
    private Dictionary<int, int> dateDecDFS;
    private Dictionary<int, int> dateFinDFS;
    private int timeDFS;
    private List<int> dfsOrder;
    private bool contientCycle;
    private int connectedComponents;

    public Graphe(int[,] matrix)
    {
        adjMatrix = matrix;
        nbNoeuds = matrix.GetLength(0);
        couleurDFS = new Dictionary<int, string>();
        predDFS = new Dictionary<int, int?>();
        dateDecDFS = new Dictionary<int, int>();
        dateFinDFS = new Dictionary<int, int>();
        dfsOrder = new List<int>();
        contientCycle = false;
        connectedComponents = 0;
    }

    // 1. DFS 遍历（带访问顺序 & 环检测）
    public void DFS_Main()
    {
        for (int i = 1; i < nbNoeuds; i++)
        {
            couleurDFS[i] = "blanc";
            predDFS[i] = null;
            dateDecDFS[i] = int.MaxValue;
            dateFinDFS[i] = int.MaxValue;
        }

        timeDFS = 1;
        dfsOrder.Clear();
        contientCycle = false;
        connectedComponents = 0;

        for (int i = 1; i < nbNoeuds; i++)
        {
            if (couleurDFS[i] == "blanc")
            {
                connectedComponents++;  // 发现一个新的连通分量
                timeDFS = DFS_Rec(i, timeDFS);
            }
        }
    }

    private int DFS_Rec(int s, int date)
    {
        couleurDFS[s] = "jaune";  // 访问中
        dateDecDFS[s] = date;
        date++;
        dfsOrder.Add(s);

        for (int v = 1; v < nbNoeuds; v++)
        {
            if (adjMatrix[s, v] == 1)
            {
                if (couleurDFS[v] == "blanc") // 未访问，递归
                {
                    predDFS[v] = s;
                    date = DFS_Rec(v, date);
                }
            }
        }

        couleurDFS[s] = "rouge";  // 访问完成
        dateFinDFS[s] = date;
        date++;
        return date;
    }

    public void PrintDFSResults()
    {
        Console.WriteLine("Noeud  | Début | Fin   | Prédécesseur");
        Console.WriteLine("-----------------------------------");
        for (int i = 1; i < nbNoeuds; i++)
        {
            Console.WriteLine($"{i,-6} | {dateDecDFS[i],-5} | {dateFinDFS[i],-5} | {predDFS[i]?.ToString() ?? "-1",-12}");
        }
        Console.WriteLine("\n Ordre de visite (DFS) : " + string.Join(" - ", dfsOrder));
    }
}

