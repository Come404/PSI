using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
/*
namespace PSI_Project_Perso
{
    class Graphe
    {
        private int[,] adjMatrix;
        private int nbNoeuds;
        private Dictionary<int, string> couleur;
        private Dictionary<int, int?> pred;
        private Dictionary<int,int> dateDec;
        private Dictionary<int, int> dateFin;
        private int time;
        private List<int> dfsOrder;
        private List<int> bfsOrder;
        private bool hasCycle;
        private int connectedComponents;

        public Graphe(int[,] matrix)
        {
            adjMatrix = matrix;
            nbNoeuds = matrix.GetLength(0);

            couleur = new Dictionary<int, string>();
            pred = new Dictionary<int, int?>();
            dateDec = new Dictionary<int, int>();
            dateFin = new Dictionary<int,int>();
            dfsOrder = new List<int>();
            bfsOrder = new List<int>();
            hasCycle = false;
            connectedComponents = 0;
        }

        // 1. DFS 遍历（带访问顺序 & 环检测）
        public void DFS_Main()
        {
            foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
            {
                couleur[i] = "blanc";
                pred[i] = null;
                dateDec[i] = int.MaxValue;
                dateFin[i] = int.MaxValue;
            }

            time = 1;
            dfsOrder.Clear();
            hasCycle = false;
            connectedComponents = 0;

            foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
            {
                if (couleur[i] == "blanc")
                {
                    connectedComponents++;  // 发现一个新的连通分量
                    DFS_Rec(i, -1);
                }
            }
        }

        private void DFS_Rec(int s, int parent)
        {
            couleur[s] = "jaune";  // 访问中
            dateDec[s] = time++;
            dfsOrder.Add(s);  // 记录访问顺序

            for (int v = 1; v < nbNoeuds; v++)
            {
                if (adjMatrix[s, v] == 1)
                {
                    if (couleur[v] == "blanc") // 未访问，递归
                    {
                        pred[v] = s;
                        DFS_Rec(v, s);
                    }
                    else if (couleur[v] == "jaune" && v != parent) // 无向图的环检测
                    {
                        hasCycle = true;
                    }
                }
            }

            couleur[s] = "rouge";  // 访问完成
            dateFin[s] = time++;
        }

        public void PrintDFSResults()
        {
            Console.WriteLine("Noeud | Début | Fin | Prédécesseur");
            foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
            {
                Console.WriteLine($"{i} | {dateDec[i]} | {dateFin[i]} | {pred[i] ?? -1}");
            }
            Console.WriteLine("\nOrdre de visite (DFS) : " + string.Join(" -> ", dfsOrder));
        }

        //  2. BFS 遍历（打印访问顺序）
        public void BFS(int start)
        {
            foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
            {
                couleur[i] = "blanc";
                pred[i] = null;
            }

            Queue<int> queue = new Queue<int>();
            bfsOrder.Clear();

            couleur[start] = "jaune";
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int s = queue.Dequeue();
                bfsOrder.Add(s);  // 记录访问顺序

                for (int v = 1; v < nbNoeuds; v++)
                {
                    if (adjMatrix[s, v] == 1 && couleur[v] == "blanc")
                    {
                        couleur[v] = "jaune";
                        pred[v] = s;
                        queue.Enqueue(v);
                    }
                }

                couleur[s] = "rouge";
            }

            Console.WriteLine("\nOrdre de visite (BFS) : " + string.Join(" -> ", bfsOrder));
        }

        public void PrintBFSResults()
        {
            Console.WriteLine("Noeud | Prédécesseur");
            foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
            {
                Console.WriteLine($"{i} | {pred[i] ?? -1}");
            }
        }

        // 3. 检测连通性
        public bool IsConnected()
        {
            return connectedComponents == 1;  // 只有一个连通分量说明是连通图
        }

        //  4. 检测环
        public bool HasCycle()
        {
            return hasCycle;
        }

        //  5. 计算连通分量个数
        public int ConnectedComponents()
        {
            return connectedComponents;
        }
    }
}

*/

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
    private bool hasCycle;
    private int connectedComponents;

    // 用于 BFS 的数据
    private Dictionary<int, string> couleurBFS;
    private Dictionary<int, int?> predBFS;
    private Dictionary<int, int> dateDecBFS;
    private int timeBFS;
    private List<int> bfsOrder;

    public Graphe(int[,] matrix)
    {
        adjMatrix = matrix;
        nbNoeuds = matrix.GetLength(0);

        couleurDFS = new Dictionary<int, string>();
        predDFS = new Dictionary<int, int?>();
        dateDecDFS = new Dictionary<int, int>();
        dateFinDFS = new Dictionary<int, int>();
        dfsOrder = new List<int>();
        hasCycle = false;
        connectedComponents = 0;

        couleurBFS = new Dictionary<int, string>();
        predBFS = new Dictionary<int, int?>();
        dateDecBFS = new Dictionary<int, int>();
        bfsOrder = new List<int>();
    }

    // 1. DFS 遍历（带访问顺序 & 环检测）
    public void DFS_Main()
    {
        foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
        {
            couleurDFS[i] = "blanc";
            predDFS[i] = null;
            dateDecDFS[i] = int.MaxValue;
            dateFinDFS[i] = int.MaxValue;
        }

        timeDFS = 1;
        dfsOrder.Clear();
        hasCycle = false;
        connectedComponents = 0;

        foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
        {
            if (couleurDFS[i] == "blanc")
            {
                connectedComponents++;  // 发现一个新的连通分量
                DFS_Rec(i, -1);
            }
        }
    }

    private void DFS_Rec(int s, int parent)
    {
        couleurDFS[s] = "jaune";  // 访问中
        dateDecDFS[s] = timeDFS++;
        dfsOrder.Add(s);  // 记录访问顺序

        for (int v = 1; v < nbNoeuds; v++)
        {
            if (adjMatrix[s, v] == 1)
            {
                if (couleurDFS[v] == "blanc") // 未访问，递归
                {
                    predDFS[v] = s;
                    DFS_Rec(v, s);
                }
                else if (couleurDFS[v] == "jaune" && v != parent) // 无向图的环检测
                {
                    hasCycle = true;
                }
            }
        }

        couleurDFS[s] = "rouge";  // 访问完成
        dateFinDFS[s] = timeDFS++;
    }

    public void PrintDFSResults()
    {
        Console.WriteLine("Noeud | Début | Fin | Prédécesseur");
        foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
        {
            Console.WriteLine($"{i} | {dateDecDFS[i]} | {dateFinDFS[i]} | {predDFS[i] ?? -1}");
        }
        Console.WriteLine("\nOrdre de visite (DFS) : " + string.Join(" -> ", dfsOrder));
    }

    //  2. BFS 遍历（打印访问顺序）
    public void BFS(int start)
    {
        foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
        {
            couleurBFS[i] = "blanc";
            predBFS[i] = null;
        }

        Queue<int> queue = new Queue<int>();
        bfsOrder.Clear();

        couleurBFS[start] = "jaune";
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int s = queue.Dequeue();
            bfsOrder.Add(s);  // 记录访问顺序

            for (int v = 1; v < nbNoeuds; v++)
            {
                if (adjMatrix[s, v] == 1 && couleurBFS[v] == "blanc")
                {
                    couleurBFS[v] = "jaune";
                    predBFS[v] = s;
                    queue.Enqueue(v);
                }
            }

            couleurBFS[s] = "rouge";
        }

        Console.WriteLine("\nOrdre de visite (BFS) : " + string.Join(" -> ", bfsOrder));
    }

    public void PrintBFSResults()
    {
        Console.WriteLine("Noeud | Prédécesseur");
        foreach (var i in Enumerable.Range(1, nbNoeuds - 1))
        {
            Console.WriteLine($"{i} | {predBFS[i] ?? -1}");
        }
    }

    // 3. 检测连通性
    public bool IsConnected()
    {
        return connectedComponents == 1;  // 只有一个连通分量说明是连通图
    }

    //  4. 检测环
    public bool HasCycle()
    {
        return hasCycle;
    }

    //  5. 计算连通分量个数
    public int ConnectedComponents()
    {
        return connectedComponents;
    }
}
