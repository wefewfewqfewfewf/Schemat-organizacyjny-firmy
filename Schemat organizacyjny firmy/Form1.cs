namespace Schemat_organizacyjny_firmy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Odczytywansko();
        }

        private void dodajGalazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            Form2 dialog = new Form2();
            dialog.Text = "Dodawanie ";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                treeView1.Nodes.Add(dialog.nazwa);
            }
        }

        private void dodajPodgalazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Form2 dialog = new Form2();
            dialog.Text = "Dodawanie";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                treeView1.SelectedNode.Nodes.Add(dialog.nazwa);
            }
        }

        private void usuñToolStripMenuItem_Click(object sender, EventArgs e)
        {

            treeView1.Nodes.Remove(treeView1.SelectedNode);

        }

        private void zmieñToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Form2 dialog = new Form2();
            dialog.Text = "Modyfikacja";
            dialog.nazwa = treeView1.SelectedNode.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                treeView1.SelectedNode.Text = dialog.nazwa;
            }
        }
        public class myNode
        {
            public string rodzic;
            public string nazwa;
            public myNode(string rodzic, string nazwa)
            {
                this.rodzic = rodzic;
                this.nazwa = nazwa;
            }
        }
        private void DodajDoListy(TreeNode node, ref List<myNode> bruh
            )
        {
            if (node == null)
                return;
            string d, k;
            if (node.Parent == null)
                d = "nie ma";
            else
                d = node.Parent.Text;
            k = node.Text;

           bruh.Add(new myNode(d, k));
            if (node.NextNode != null)
                DodajDoListy(node.NextNode, ref bruh);
            if (node.GetNodeCount(true) > 0)
                DodajDoListy(node.FirstNode, ref bruh);
        }
        private void Zapisywansko()
        {
            List<myNode> bruh= new List<myNode>();
            DodajDoListy(treeView1.Nodes[0], ref bruh);
            if (bruh.Count == 0)
                return;

            string meh = " ";
            foreach (myNode elem in bruh)
            {
                meh += elem.nazwa + " " + elem.rodzic + "\n";
            }
            File.WriteAllText("firma.txt", meh);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Zapisywansko();
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Zapisaæ ?",
                "Zamykam", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                Zapisywansko();
                Application.Exit();
            }
            else if (res == DialogResult.No)
            {
                Application.Exit();
            }

        }
        private void Odczytywansko()
        {
            treeView1.Nodes.Clear();
            List<myNode> lista = new List<myNode>();
            if (!File.Exists("firma.txt")) return;
            string[] tab = File.ReadAllLines("firma.txt");
            foreach (string elem in tab)
            {
                string[] pom = elem.Split(' ');
                lista.Add(new myNode(pom[1], pom[0]));
            }
            foreach(myNode node in lista)
            {
                if (node.rodzic == "brak")
                    treeView1.Nodes.Add(new TreeNode(node.nazwa));
                else
                {
                    TreeNode rodzic = ZnajdzRodzica(node.rodzic);
                        if (rodzic != null)
                        rodzic.Nodes.Add(node.nazwa);
                }
            }

        }
        private TreeNode ZnajdzRodzica(string rodzic)
        {
            return null;
        }
    }
}