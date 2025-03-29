//Hello Groupe !!!namespace PSI_Project_Perso
namespace PSI_Project_Perso
{
    public class Noeud
    {
        public int Id;
        public List<int> Voisins;
        public string Couleur;   // "blanc", "jaune", "rouge"
        public int? Pred; // Écriture correcte du type Nullable // Nœuds prédécesseurs 
        public int DateDec;      // Date de découverte du noeud
        public int DateFin;      // Date de fin de traitement

        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Créé un noeud avec un Id unique; constructeur naturel de la classe Noeud
        /// </summary>
        public Noeud(int id)
        {
            Id = id;
            Voisins = new List<int>();
            Couleur = "blanc";
            Pred = null;
            DateDec = int.MaxValue;
            DateFin = int.MaxValue;
            Random rnd = new Random();
            X = rnd.Next(50, 350);
            Y = rnd.Next(50, 350);
        }
        /// <summary>
        /// Permet de rajouter un noeud adjacent à la liste d'adjacence d'une instance de Noeud 
        /// </summary>
        public void Rajouter(int voisin)
        {
            if (!Voisins.Contains(voisin))
                Voisins.Add(voisin);
        }
    }
}
