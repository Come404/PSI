using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Windows.Forms;
using Avalonia.Media.TextFormatting.Unicode;

namespace PSI_Project_Perso
{
    public class GraphForm : Form
    {
        private Graphe graph; // Référence au graphe à afficher
        private Dictionary<int, Point> nodePositions; // Dictionnaire stockant les positions des nœuds
        private Random rnd; // Générateur de nombres aléatoires pour positionner les nœuds

        /// <summary>
        /// Constructeur de la classe GraphForm.
        /// Initialise la fenêtre d'affichage du graphe.
        /// </summary>
        /// <param name="graph">Le graphe à afficher</param>
        public GraphForm(Graphe graph)
        {
            this.graph = graph;
            this.Text = "Visualisation du Graphe"; // Définition du titre de la fenêtre
            this.Size = new Size(600, 600); // Taille de la fenêtre
            this.Paint += new PaintEventHandler(GraphForm_Paint); // Gestionnaire d'événement pour le dessin du graphe
            this.rnd = new Random();
            this.nodePositions = new Dictionary<int, Point>();

            AssignRandomPositions(); // Attribution de positions aléatoires aux nœuds
        }

        /// <summary>
        /// Assigne des positions aléatoires aux nœuds dans la fenêtre.
        /// </summary>
        private void AssignRandomPositions()
        {
            foreach (var noeud in graph.GetNoeuds())
            {
                int x = rnd.Next(50, 550); // Génération de la coordonnée X
                int y = rnd.Next(50, 550); // Génération de la coordonnée Y
                nodePositions[noeud.Id] = new Point(x, y); // Enregistrement de la position
            }
        }

        /// <summary>
        /// Gère l'événement Paint pour dessiner le graphe.
        /// </summary>
        private void GraphForm_Paint(object sender, PaintEventArgs e)
        {
            // Liste des nœuds sans le nœud 0
            List<Noeud> noeuds = graph.GetNoeuds().Where(n => n.Id != 0).ToList(); // 🔥 Exclure le nœud '0'
            // Liste des liens sans ceux connectés au nœud 0
            List<Lien> liens = graph.GetLiens().Where(l => l.Noeud1.Id != 0 && l.Noeud2.Id != 0).ToList(); // 🔥 Exclure les liens avec le nœud '0'

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // ✅ Lissage des contours pour un rendu plus fluide

            // Définition des styles graphiques
            Pen edgePen = new Pen(Color.DarkGray, 2); // Stylo pour les arêtes du graphe
            Brush nodeBrush = new SolidBrush(Color.CornflowerBlue); // Couleur des nœuds
            Font font = new Font("Arial", 14, FontStyle.Bold); // Police pour les numéros des nœuds
            Brush textBrush = new SolidBrush(Color.White); // Couleur du texte

            // ✅ Positionner les nœuds selon une disposition circulaire
            int centerX = this.Width / 2; // Centre horizontal de la fenêtre
            int centerY = this.Height / 2; // Centre vertical de la fenêtre
            int radius = Math.Min(centerX, centerY) - 50; // Rayon du cercle où seront placés les nœuds
            int nodeCount = noeuds.Count; // Nombre de nœuds à afficher

            Dictionary<int, PointF> positions = new Dictionary<int, PointF>(); // Dictionnaire des positions des nœuds

            // Calcul des positions des nœuds en cercle
            for (int i = 0; i < nodeCount; i++)
            {
                double angle = (2 * Math.PI * i) / nodeCount; // Angle du nœud sur le cercle
                float x = centerX + (float)(radius * Math.Cos(angle)); // Coordonnée X
                float y = centerY + (float)(radius * Math.Sin(angle)); // Coordonnée Y

                positions[noeuds[i].Id] = new PointF(x, y); // Stockage de la position
            }

            // ✅ Dessiner d'abord les arêtes (liens entre nœuds)
            foreach (Lien lien in liens)
            {
                if (lien.Noeud1 == null || lien.Noeud2 == null) continue; // Vérification des données

                PointF p1 = positions[lien.Noeud1.Id]; // Position du premier nœud
                PointF p2 = positions[lien.Noeud2.Id]; // Position du deuxième nœud

                g.DrawLine(edgePen, p1, p2); // Dessin de l'arête
            }

            // ✅ Dessiner ensuite les nœuds
            foreach (Noeud noeud in noeuds)
            {
                if (noeud == null) continue; // Vérification du nœud

                PointF pos = positions[noeud.Id]; // Récupération de la position du nœud
                float size = 30; // Taille du cercle représentant le nœud
                g.FillEllipse(nodeBrush, pos.X - size / 2, pos.Y - size / 2, size, size); // Dessin du nœud
                g.DrawString(noeud.Id.ToString(), font, textBrush, pos.X - 10, pos.Y - 10); // Affichage de l'ID du nœud
            }
        }
    }
}
