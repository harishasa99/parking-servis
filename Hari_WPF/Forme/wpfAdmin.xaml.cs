using Hari_WPF.Forme.Commands;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hari_WPF.Forme
{
    /// <summary>
    /// Interaction logic for wpfAdmin.xaml
    /// </summary>
    public partial class wpfAdmin : Window, INotifyPropertyChanged
    {
        public string TNovca { get; set; }

        public wpfAdmin()
        {
            InitializeComponent();
            DataContext = this;
            BrisiKorisnike1 = new Commanda(BrisiKorisnike);
        }

        public Commands.Commanda BrisiKorisnike1 { get; set; }


        public void BrisiKorisnike()
        {
            MessageBoxResult xx = MessageBox.Show("Zelite izbrisati sve korisinike !", "Sigurni?", MessageBoxButton.YesNo);
            if (xx == MessageBoxResult.No) return;
            var all = from c in HE.Vozilas select c;

            HE.Vozilas.RemoveRange(all);
            HE.SaveChanges();
            dgKorisnici.ItemsSource = HE.Vozilas.ToList<Vozila>();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            haris_bazaEntities HE = new haris_bazaEntities();

            dgEvidencija.ItemsSource = HE.Evidencijas.ToList<Evidencija>();
            dgKorisnici.ItemsSource = HE.Vozilas.ToList<Vozila>();
            txtCena.Text = Class1.cenaParkinga.ToString();
            txtCenaPopust.Text = Class1.cenaPromo.ToString();
            txtPromoParking.Text = Class1.brojPromoParkiranja.ToString();
           
            Ukupno_Novca();          
        }

        void Ukupno_Novca()
        {
            double? unovca = HE.Evidencijas.Sum(x => x.naplaceno);
            TNovca = "UKUPNO NAPLACENO: " + unovca.ToString();
            NotifikacijaPromene(nameof(TNovca));
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Class1.cenaParkinga = double.Parse(txtCena.Text);
            Class1.cenaPromo = double.Parse(txtCenaPopust.Text);
            Class1.brojPromoParkiranja = int.Parse(txtPromoParking.Text);
            Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            //  haris_bazaEntities HE = new haris_bazaEntities();
            Close();
        }


        haris_bazaEntities HE = new haris_bazaEntities();


        public void NotifikacijaPromene(string ImePromenljive)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ImePromenljive));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void DataGrid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Enter || Key.Tab == e.Key)
            {
                e.Handled = true;

                dgEvidencija.CommitEdit();
                dgEvidencija.CommitEdit();

                Evidencija novoP = (sender as DataGridRow).Item as Evidencija;

                Evidencija staroP = HE.Evidencijas.Where(t => t.id.Equals(novoP.id)).FirstOrDefault();
                if (staroP is null) return;
                              
                staroP.br_tablica = novoP.br_tablica;
                staroP.vlasnik=novoP.vlasnik;
                staroP.naplaceno=novoP.naplaceno;   
                staroP.tip_vozila=novoP.tip_vozila;
                staroP.vreme_provedeno = novoP.vreme_provedeno;
                HE.SaveChanges();

                Ukupno_Novca();

                MessageBox.Show("Promena zavrsena!");

                //              HE.Evidencijas.Remove(old);
                ////  HE.SaveChanges();
                // MessageBox.Show(newP.br_tablica + "  " + old.br_tablica);
                //  //Check for duplication  and Add the new Items  
                //  foreach (Evidencija p in dgEvidencija.Items)
                //  {
                //      if (p.id != newP.id) { HE.Evidencijas.Add(newP); HE.SaveChanges(); }
                //  }

            }
            else if (e.Key is Key.Delete)
            {
                Evidencija novoP = (sender as DataGridRow).Item as Evidencija;
                Evidencija staroP = HE.Evidencijas.Where(t => t.id.Equals(novoP.id)).FirstOrDefault();
                if (staroP is null) return;
               
                HE.Evidencijas.Remove(staroP);
                HE.SaveChanges() ;

                Ukupno_Novca();

                MessageBox.Show("Uspesno izbrisana stavka evidencije!");
            }
        }

        private void dgEvidencija_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // MessageBox.Show (e.Row.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult xx = MessageBox.Show("Zelite izbrisati sve !", "Sigurni?", MessageBoxButton.YesNo);
            if (xx == MessageBoxResult.No) return;
            var all = from c in HE.Evidencijas select c;

            HE.Evidencijas.RemoveRange(all);
            HE.SaveChanges();
            dgEvidencija.ItemsSource = HE.Evidencijas.ToList<Evidencija>();
            //Close();
            Ukupno_Novca();
        }

        private void DataGridRow_KeyDown(object sender, KeyEventArgs e) // promena i brisanje korisnika
        {
            if (e.Key == Key.Enter || Key.Tab == e.Key)
            {
                e.Handled = true;

                dgKorisnici.CommitEdit();
                dgKorisnici.CommitEdit();

                Vozila novoP = (sender as DataGridRow).Item as Vozila;

                Vozila staroP = HE.Vozilas.Where(t => t.kor_ime.Equals(novoP.kor_ime)).FirstOrDefault();
                if (staroP is null) return;

                staroP.sifra = novoP.sifra;
                staroP.vlasnik = novoP.vlasnik;
                staroP.tip_vozila = novoP.tip_vozila;
                staroP.br_tablica = novoP.br_tablica;               
                HE.SaveChanges();
                MessageBox.Show("Promena korisnika zavrsena!");

            }
            else if (e.Key is Key.Delete)
            {
                Vozila novoP = (sender as DataGridRow).Item as Vozila;
                Vozila staroP = HE.Vozilas.Where(t => t.kor_ime.Equals(novoP.kor_ime)).FirstOrDefault();
                if (staroP is null) return;
               
                HE.Vozilas.Remove(staroP);
                HE.SaveChanges();
                MessageBox.Show("Uspesno izbrisan Korsinik!");
            }
        }
    }
}

