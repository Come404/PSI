using System;
using System.Collections.Generic;
namespace PSI_Project_Perso
{
    class Graphe
    {
        private int[,] MatriceAdj;
        private List<Noeud> noeuds;
        private bool contientCycle;
        private List<Lien> liens;

        public bool ContientCycle
        {
            get { return contientCycle; }
        }

        public Graphe(List<(int, int)> arc)
        {
            int nbNoeuds = 0;
            for (int i = 0; i < arc.Count; i++)
            {
                int noeud1 = arc[i].Item1;
                int noeud2 = arc[i].Item2;

                if (noeud1 > nbNoeuds)
                {
                    nbNoeuds = noeud1;
                }
                if (noeud2 > nbNoeuds)
                {
                    nbNoeuds = noeud2;
                }
            }

            MatriceAdj = new int[nbNoeuds + 1, nbNoeuds + 1];
            noeuds = new List<Noeud>();
            liens = new List<Lien>();

            for (int i = 0; i <= nbNoeuds; i++)
            {
                noeuds.Add(new Noeud(i));
            }

            for (int i = 0; i < arc.Count; i++)
            {
                int noeud1 = arc[i].Item1;
                int noeud2 = arc[i].Item2;

                MatriceAdj[noeud1, noeud2] = 1;
                MatriceAdj[noeud2, noeud1] = 1;

                // Relation de voisin
                noeuds[noeud1].Rajouter(noeud2);
                noeuds[noeud2].Rajouter(noeud1);

                liens.Add(new Lien(noeuds[noeud1], noeuds[noeud2]));
            }

        }

        /// <summary>
        /// 
        /// Afficher la matrice d'adjacence
        /// </summary>
        public void AfficherMatrice()
        {
            Console.WriteLine("\nMatrice d'adjacence:");
            for (int i = 1; i < MatriceAdj.GetLength(0); i++)
            {
                for (int j = 1; j < MatriceAdj.GetLength(1); j++)
                {
                    Console.Write(MatriceAdj[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Tester si la matrice est orientee
        /// </summary>
        /// <returns></returns>
        public bool EstNonOriente()
        {
            for (int i = 1; i < MatriceAdj.GetLength(0); i++)
            {
                for (int j = 1; j < MatriceAdj.GetLength(1); j++)
                {
                    if (MatriceAdj[i, j] != MatriceAdj[j, i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Executer le DFS
        /// </summary>
        public void DFS_Main()
        {
            int connectedComponents = 0;
            List<int> dfsOrder = new List<int>();

            foreach (Noeud noeud in noeuds)
            {
                noeud.Couleur = "blanc";
            }

            foreach (Noeud noeud in noeuds)
            {
                if (noeud.Id > 0 && noeud.Couleur == "blanc")
                {
                    connectedComponents++;
                    DFS_Rec(noeud, dfsOrder, ref contientCycle);
                }
            }

            Console.WriteLine("\nNombre de composants connexes: " + connectedComponents);

            if (connectedComponents == 1)
                Console.WriteLine("\nLe graphe est connexe.\n");
            else
                Console.WriteLine("\nLe graphe n'est pas connexe.\n");


            Console.WriteLine("\nOrdre de visite (DFS) : " + string.Join(" -> ", dfsOrder));
        }

        private void DFS_Rec(Noeud noeud, List<int> dfsOrder, ref bool contientCycle)
        {
            dfsOrder.Add(noeud.Id);
            noeud.Couleur = "jaune";

            foreach (int voisinId in noeud.Voisins)
            {
                Noeud voisin = noeuds[voisinId];
                if (voisin.Couleur == "blanc")
                {
                    DFS_Rec(voisin, dfsOrder, ref contientCycle);
                }
                else if (voisin.Couleur == "jaune")
                {
                    contientCycle = true;
                }
            }
            noeud.Couleur = "rouge";
        }

        /// <summary>
        /// Executer le BFS
        /// </summary>
        /// <param name="origine"></param>
        /// <returns></returns>
        public List<int> BFS(int origine)
        {
            Queue<int> aTraiter = new Queue<int>();
            List<int> bfsOrder = new List<int>();

            foreach (Noeud noeud in noeuds)
            {
                noeud.Couleur = "blanc";
                noeud.Pred = null;
            }

            noeuds[origine].Couleur = "jaune";
            aTraiter.Enqueue(origine);

            while (aTraiter.Count > 0)
            {
                int n = aTraiter.Dequeue();
                bfsOrder.Add(n);

                foreach (int voisinId in noeuds[n].Voisins)
                {
                    Noeud voisin = noeuds[voisinId];
                    if (voisin.Couleur == "blanc")
                    {
                        voisin.Couleur = "jaune";
                        voisin.Pred = n;
                        aTraiter.Enqueue(voisinId);
                    }
                }
                noeuds[n].Couleur = "rouge";
            }
            return bfsOrder;
        }

        public void AfficherBFSOrder(List<int> bfsOrder)
        {
            Console.Write("\nOrdre de visite (BFS) : " + string.Join(" -> ", bfsOrder));
            Console.WriteLine();
        }

        public int TailleDeGraphe()
        {
            return liens.Count;
        }

        public int OrdreDeGraphe()
        {
            return noeuds.Count - 1;
        }

    }
}


