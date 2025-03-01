namespace PSI_Project_Perso
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    public class GraphForm : Form
    {
        private Graphe graph;
        private Dictionary<int, Point> nodePositions;
        private Random rnd;

        public GraphForm(Graphe graph)
        {

            this.graph = graph;
            if (graph.GetNoeuds() == null || graph.GetLiens() == null)
            {
                MessageBox.Show("Graph data is null! Check the initialization.");
                return;
            }
            this.Text = "Graph Visualization";
            this.Size = new Size(600, 600);
            this.Paint += new PaintEventHandler(GraphForm_Paint);
            this.rnd = new Random();
            this.nodePositions = new Dictionary<int, Point>();

            AssignRandomPositions();
        }

        private void AssignRandomPositions()
        {
            foreach (var noeud in graph.GetNoeuds())
            {
                int x = rnd.Next(50, 550);
                int y = rnd.Next(50, 550);
                nodePositions[noeud.Id] = new Point(x, y);
            }
        }

        private void GraphForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen edgePen = new Pen(Color.Black, 2);
            Font font = new Font("Arial", 12);
            Brush brushBlanc = Brushes.Blue;
            Brush brushJaune = Brushes.Yellow;
            Brush brushRouge = Brushes.Red;

            // Draw edges
            foreach (Lien lien in graph.GetLiens())
            {
                Point p1 = nodePositions[lien.Noeud1.Id];
                Point p2 = nodePositions[lien.Noeud2.Id];
                g.DrawLine(edgePen, p1, p2);
            }

            // Draw nodes
            foreach (Noeud noeud in graph.GetNoeuds())
            {
                Point p = nodePositions[noeud.Id];
                Brush nodeBrush = noeud.Couleur switch
                {
                    "jaune" => brushJaune,
                    "rouge" => brushRouge,
                    _ => brushBlanc,
                };

                g.FillEllipse(nodeBrush, p.X - 15, p.Y - 15, 30, 30);
                g.DrawString(noeud.Id.ToString(), font, Brushes.White, p.X - 7, p.Y - 7);
            }
        }
    }

}
