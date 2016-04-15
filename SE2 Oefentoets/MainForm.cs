using System;
using System.Windows.Forms;

namespace SE2_Oefentoets
{
    public partial class MainForm : Form
    {
        private readonly Voorraad _voorraad = new Voorraad();
        private decimal _inworp = new decimal(7.50);

        public MainForm()
        {
            InitializeComponent();

            // Event handlers
            btnEuro020.Click += btnInworp_click;
            btnEuro050.Click += btnInworp_click;
            btnEuro100.Click += btnInworp_click;
            btnEuro200.Click += btnInworp_click;

            RefreshData();
        }

        private void btnInworp_click(object sender, EventArgs eventArgs)
        {
            if (sender == btnEuro020)
            {
                _inworp += (decimal) 0.20;
            }
            else if (sender == btnEuro050)
            {
                _inworp += (decimal) 0.50;
            }
            else if (sender == btnEuro100)
            {
                _inworp += (decimal) 1.00;
            }
            else if (sender == btnEuro200)
            {
                _inworp += (decimal) 2.00;
            }

            RefreshData();
        }

        private void btnServeer_Click(object sender, EventArgs e)
        {
            Drank drank = lbDranken.SelectedItem as Drank;

            try
            {
                // Koop een drank als deze geselecteerd is
                if (drank != null)
                {
                    if (_voorraad.KoopDrank(drank, _inworp))
                    {
                        // Schrijf geld af als het kopen gelukt is
                        _inworp -= drank.Prijs;
                    }
                    else
                    {
                        MessageBox.Show("Niet genoeg geld");
                    }
                }
                else
                {
                    MessageBox.Show("Geen drank geselecteerd");
                }
            }
            catch (OnvoldoendeBekersException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                MessageBox.Show(ex.Message);
            }

            RefreshData();
        }

        private void btnDrankToevoegen_Click(object sender, EventArgs e)
        {
            string naam = tbDrankNaam.Text;
            decimal prijs = nudDrankPrijs.Value;
            int milliliter = Convert.ToInt32(nudDrankMilliliter.Value);
            int voedingswaarde = Convert.ToInt32(nudDrankVoedingswaarde.Value);

            switch (cbDrankSoort.Text)
            {
                case "Frisdrank":
                    if (naam != "" && prijs >= 0 && milliliter > 0 && voedingswaarde >= 0)
                    {
                        _voorraad.NieuwProduct(new Frisdrank(naam, prijs, milliliter, voedingswaarde));
                    }
                    else
                    {
                        MessageBox.Show("Ongeldige data");
                    }
                    break;
                case "Koffie":
                    if (naam != "" && prijs >= 0 && milliliter > 0 && voedingswaarde >= 0)
                    {
                        _voorraad.NieuwProduct(new Koffie(naam, prijs, milliliter, voedingswaarde));
                    }
                    else
                    {
                        MessageBox.Show("Ongeldige data");
                    }
                    break;
                case "Soep":
                    if (naam != "" && prijs >= 0 && milliliter > 0)
                    {
                        _voorraad.NieuwProduct(new Soep(naam, prijs, milliliter));
                    }
                    else
                    {
                        MessageBox.Show("Ongeldige data");
                    }
                    break;
                default:
                    MessageBox.Show("Geen product geselecteerd");
                    break;
            }

            RefreshData();
        }

        private void btnVoorraadToevoegen_Click(object sender, EventArgs e)
        {
            int aantal = Convert.ToInt32(nudVoorraadAantal.Value);

            IVoorraad voorraad = lbVoorraad.SelectedItem as IVoorraad;
            if (voorraad != null)
            {
                _voorraad.VulBij(voorraad, aantal);
            }
            else
            {
                MessageBox.Show("Geen voorraad geselecteerd");
            }

            RefreshData();
        }

        private void btnBekerToevoegen_Click(object sender, EventArgs e)
        {
            string naam = tbBekerNaam.Text;
            int milliliters = Convert.ToInt32(nudBekerMilliliter.Value);
            bool warmeDrankMogelijk = chkBekerWarmeDrank.Checked;

            if (naam != "" && milliliters > 0)
            {
                _voorraad.NieuwProduct(new Beker(naam, milliliters, warmeDrankMogelijk));
            }
            else
            {
                MessageBox.Show("Ongeldige data");
            }

            RefreshData();
        }

        private void btnExporteerLogbestand_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                DefaultExt = "log",
                Filter = "Log bestand|*.log"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _voorraad.ExporteerLogbestand(dialog.FileName);
            }
        }

        private void RefreshData()
        {
            object selectedVoorraad = lbVoorraad.SelectedItem;
            lbVoorraad.Items.Clear();
            _voorraad.BeschikbareProducten().ForEach(voorraad => lbVoorraad.Items.Add(voorraad));
            if (selectedVoorraad != null && lbVoorraad.Items.Contains(selectedVoorraad))
            {
                lbVoorraad.SelectedItem = selectedVoorraad;
            }

            object selectedDrank = lbDranken.SelectedItem;
            lbDranken.Items.Clear();
            _voorraad.VoorradigeDranken().ForEach(drank => lbDranken.Items.Add(drank));
            if (selectedDrank != null && lbDranken.Items.Contains(selectedDrank))
            {
                lbDranken.SelectedItem = selectedDrank;
            }

            lblInworp.Text = _inworp.ToString("##.00");
        }
    }
}