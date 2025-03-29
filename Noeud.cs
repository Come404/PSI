//Hello Groupe !!!namespace PSI_Project_Perso
namespace PSI_Project_Perso
{
    internal class Noeud
    {
        public int Id;
        public List<int> Voisins;
        public string Couleur;   // "blanc", "jaune", "rouge"
        public int? Pred;        // peut-etre null
        public int DateDec;      
        public int DateFin;      

        public Noeud(int id)
        {
            Id = id;
            Voisins = new List<int>();
            Couleur = "blanc";
            Pred = null;
            DateDec = int.MaxValue;
            DateFin = int.MaxValue;
        }

        public void Rajouter(int voisin)
        {
            if (!Voisins.Contains(voisin))
                Voisins.Add(voisin);
        }
    }
}
