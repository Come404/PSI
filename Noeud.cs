//Hello Groupe !!!
namespace PSI_Project_Perso
{
    internal class Noeud
    {
        public int Id;
        public List<int> Voisins;

        public Noeud(int id)
        {
            Id = id;
            Voisins = new List<int>();
        }

        public void Rajouter(int b)
        {
            this.Voisins.Add(b);
        }
    }
}

