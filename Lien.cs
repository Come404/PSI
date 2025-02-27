namespace PSI_Project_Perso
{
    internal class Lien
    {
        public Noeud Noeud1 { get; }
        public Noeud Noeud2 { get; }

        public Lien(Noeud noeud1, Noeud noeud2)
        {
            Noeud1 = noeud1;
            Noeud2 = noeud2;
        }

        public override string ToString()
        {
            return $"{Noeud1.Id} - {Noeud2.Id}";
        }
    }
}
