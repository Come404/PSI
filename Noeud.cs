//Hello Groupe !!!namespace PSI_Project_Perso
namespace PSI_Project_Perso
{
    internal class Noeud
    {
        public int Id;
        public List<int> Voisins;
        public string Couleur;   // "blanc", "jaune", "rouge"
        public int? Pred;        // 前驱节点
        public int DateDec;      // 发现时间
        public int DateFin;      // 完成时间

        public int X { get; set; }
        public int Y { get; set; }

        public Noeud(int id)
        {
            Random rnd = new Random();
            Id = id;
            Voisins = new List<int>();
            Couleur = "blanc";
            Pred = null;
            DateDec = int.MaxValue;
            DateFin = int.MaxValue;
            X = rnd.Next(50, 350); 
            Y = rnd.Next(50, 350);
        }

        public void Rajouter(int voisin)
        {
            if (!Voisins.Contains(voisin))
                Voisins.Add(voisin);
        }
    }
}
