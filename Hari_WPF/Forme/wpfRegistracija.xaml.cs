using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hari_WPF.Forme
{
    /// <summary>
    /// Interaction logic for wpfRegistracija.xaml
    /// </summary>
    public partial class wpfRegistracija : Window
    {      

        public  Vozila vo { get; set; } =new Vozila();  

        public wpfRegistracija()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (vo.kor_ime == string.Empty || txtSifra.Password == string.Empty || vo.vlasnik == string.Empty || vo.br_tablica == string.Empty || vo.tip_vozila == string.Empty)
            {
                MessageBox.Show("Moraju biti popunjena sva polja!");
                return;
            }

            
            haris_bazaEntities cc = new haris_bazaEntities();
            Vozila voz =cc.Vozilas.Find(vo.kor_ime);
            if (voz == null)
            {           
                vo.sifra= txtSifra.Password;
                cc.Vozilas.Add(vo);
                cc.SaveChanges();
                MessageBox.Show("Uspesno registrovan korisnik!");
            }
            else
            {
                
                MessageBox.Show("Korsnik vec postoji!");
                return;
            }

            Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {          
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbTipVozila.Items.Add("Putnicko");
            cmbTipVozila.Items.Add("Teretno");
            cmbTipVozila.Items.Add("Motocikal");
        }

      
    }
}
