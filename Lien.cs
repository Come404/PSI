namespace PSI_Project_Perso
{
    public class Lien
    {
        public Noeud Noeud1 { get; }
        public Noeud Noeud2 { get; }
        /// <summary>
        /// Créé un lien entre 2 noeuds noued1 et noeud2; constructeur naturel de la classe Lien
        /// </summary>
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
