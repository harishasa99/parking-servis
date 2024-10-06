using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Hari_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new Class1();[
        }
        public static string korisnik="GOST";

        public string Resulttt { get; set; }

        string nazikorisnika(string ime, string tip, string tablice, DateTime vreme)
        {
            string txt = Environment.NewLine + "Korisnik:" + Environment.NewLine + ime + Environment.NewLine + Environment.NewLine + "Br.Tablica:" + Environment.NewLine + tablice + System.Environment.NewLine + System.Environment.NewLine + "Tip vozila:" + Environment.NewLine + tip + Environment.NewLine + Environment.NewLine + "Vreme Ulaska:" + Environment.NewLine + vreme.ToString("HH:mm:ss");
            return txt;
        }


        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {         
          
            Label lbl = (Label)sender;
            //  lbl.Content = "sax";
           // MessageBox.Show(lbl.Name + " " + lbl.Tag);
            if (lbl.Background == Brushes.Green) // znaci parking je slobodan i upisi korisnika
            {
                var xx = MessageBox.Show("Jel ste registrovan korisnik?", "Registrovan korisnik", MessageBoxButton.YesNo);
                if (xx == MessageBoxResult.Yes)
                {
                    Forme.LoginPodaci wpfPodaci = new Forme.LoginPodaci();
                    // var rr= wpfPodaci.ShowDialog();
                  
                    
                    bool? rezultatDialoga = wpfPodaci.ShowDialog();
                    if (rezultatDialoga is true)
                    {
                        haris_bazaEntities ee = new haris_bazaEntities();
                        Vozila vv = ee.Vozilas.Find(Class1.txtKorisnickoIme);
                        if (vv != null)
                        {
                            if (vv.sifra != Class1.txtSifra)
                            {
                                MessageBox.Show("Greska u sifri!");
                                return;

                            }
                            else //  ako je sve ok ispisi korisnika ..
                            {
                                int mesto = int.Parse(lbl.Name.Substring(lbl.Name.Length - 1, 1));
                               // int mestoTag = int.Parse(lbl.Tag);
                                bool jelMoze = upisiParkingmesto(mesto, true, vv.vlasnik, vv.tip_vozila, vv.br_tablica,DateTime.Now);
                                if (jelMoze == false) return; // kornisk je vec na parkingu 

                                lbl.Background = Brushes.Red; lbl.Foreground = Brushes.White; lbl.Content = nazikorisnika(vv.vlasnik, vv.tip_vozila, vv.br_tablica,DateTime.Now);
                                slobodnoMesta -= 1;
                                IspisiSlobodnaMesta();                              
                            }

                        }
                        else
                        {
                            MessageBox.Show("Greska u korisnicko_ime !");
                        } 
                    }
                    return;
                }
                else
                {
                    int mesto = int.Parse(lbl.Name.Substring(lbl.Name.Length - 1, 1));
                    bool jelMoze = upisiParkingmesto(mesto, true, korisnik, "PUTNICKO", "XXXX",DateTime.Now);
                    if (jelMoze == false) return; // kornisk je vec na parkingu 

                    lbl.Background = Brushes.Red; lbl.Foreground = Brushes.White; lbl.Content = nazikorisnika(korisnik, "PUTNICKO", "XXXX",DateTime.Now);
                    slobodnoMesta -= 1;
                    IspisiSlobodnaMesta();
                }
              
            }
            else if (lbl.Background == Brushes.Orange) // Kad je rezervisan parking samo za datog korisnika
            {
                Forme.LoginPodaci pd= new Forme.LoginPodaci();
                bool? rDialog = pd.ShowDialog();
                if (rDialog is true)
                {
                    haris_bazaEntities ee = new haris_bazaEntities();
                    Vozila vv = ee.Vozilas.Find(Class1.txtKorisnickoIme);
                    if (vv != null)
                    {
                        if (vv.sifra != Class1.txtSifra)
                        {
                            MessageBox.Show("Greska u sifri!");
                            return;

                        }
                        else
                        {
                            ParkingMesto Pm = ee.ParkingMestoes.First(x => x.vlasnik == vv.vlasnik);
                            int mesto = int.Parse(lbl.Name.Substring(lbl.Name.Length - 1, 1));
                            if (mesto == Pm.brojMesta)
                            {
                               // lbl.Background = Brushes.Red;
                                Pm.vreme_ulaska = DateTime.Now;
                                lbl.Background = Brushes.Red; lbl.Foreground = Brushes.White; lbl.Content = nazikorisnika(Pm.vlasnik, Pm.tip_vozila, Pm.reg_broj, DateTime.Now);
                                ee.SaveChanges();
                            }
                            else
                            {
                                MessageBox.Show("ovo mesto je rezervisano za drugog korisnika! " );
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("greska u korisnickom imenu");
                    }
                }

            }
            else // oslobodi parking
            {
                
                int mesto = int.Parse(lbl.Name.Substring(lbl.Name.Length - 1, 1));
                bool reserve = oslobodiParkingMesto(mesto);

                if (reserve)
                {
                    lbl.Background = Brushes.Orange; lbl.Foreground = Brushes.Black;                   
                }
                else
                {
                    lbl.Background = Brushes.Green; lbl.Foreground = Brushes.Black; lbl.Content = "";
                    slobodnoMesta += 1;
                    IspisiSlobodnaMesta();
                }
            }
            
        }
        bool oslobodiParkingMesto(int mesto)
        {
            haris_bazaEntities he = new haris_bazaEntities();
            ParkingMesto pm = he.ParkingMestoes.First(x => x.brojMesta == mesto);

            double cena = Class1.cenaParkinga;
            bool promoC = false; // promoCena
            int koliko = he.Evidencijas.Count(x => x.vlasnik == pm.vlasnik); //kolio parkiranje je imao dati vlasnik
            koliko += 1;
            if (koliko>= Class1.brojPromoParkiranja && pm.vlasnik != "GOST") // ukljuci promociju ako na parkingu nije gost
            {
                promoC = true;
                cena = Class1.cenaPromo;
            }
           

            // --- obracun vremena za naplatu .. po sekundama radi  primera
            TimeSpan ss = (DateTime.Now - DateTime.Parse(pm.vreme_ulaska.ToString()));
            decimal ro = Math.Truncate(decimal.Parse(ss.TotalSeconds.ToString()));
            double naplaceno = int.Parse(ro.ToString()) * cena;
          
            // --- obracun vremena za naplatu .. po sekundama radi  primera


            //he.ParkingMestoes.Remove(he.ParkingMestoes.Find(mesto));
          

            Evidencija ev = new Evidencija
            {
                vlasnik=pm.vlasnik,
                br_tablica=pm.reg_broj,
                tip_vozila=pm.tip_vozila,
                vreme_ulaza=pm.vreme_ulaska,
                vreme_izlaza=DateTime.Now,
                vreme_provedeno=int.Parse ( ro.ToString()),
                naplaceno=naplaceno
            };

            he.Evidencijas.Add(ev);
            if (promoC)
                MessageBox.Show("Provedeno vreme: " + ro.ToString() + " sekundi"+ Environment.NewLine +" Naplaceno po promo ceni: " + naplaceno.ToString(), "PROMO CENA: "+ cena.ToString(),MessageBoxButton.OK,MessageBoxImage.Information);
            else
            MessageBox.Show("Provedeno vreme: " + ro.ToString() + " sekundi, Naplaceno: " + naplaceno.ToString());

            bool rezerve = false;
            if (pm.vlasnik != "GOST")
            {
                var tt = MessageBox.Show("zelite rezervisat!", "Rezerve", MessageBoxButton.YesNo);
                if (tt is MessageBoxResult.Yes)
                {
                    rezerve = true;
                    pm.statusZauzetosti = false; // ako stoji u bazi a status mu j false onda je rezervisano
                }
                else
                {
                    rezerve = false;
                  
                }
            }
           if (rezerve is false) he.ParkingMestoes.Remove(pm);

            he.SaveChanges();

           
            return rezerve;

        }
      
        bool upisiParkingmesto(int brojMesta, bool statusZauteosti, string vlasnik, string tip_vozila, string reg_broj, DateTime vreme_ulaska)
        {
            haris_bazaEntities he = new haris_bazaEntities();
            
        ParkingMesto t1 = he.ParkingMestoes.Where(x => x.vlasnik == vlasnik).FirstOrDefault<ParkingMesto>();
           // var t = from c in he.ParkingMestoes where c.vlasnik == vlasnik select c;
           // MessageBox.Show(t.Expression.ToString());
            if (t1!=null && vlasnik !="GOST")
            {
                MessageBox.Show("korsnik je vec na parkingu! ");
                return false;
            }
            
            ParkingMesto pm = new ParkingMesto { 
                brojMesta=brojMesta,
                lokacija=brojMesta,
                statusZauzetosti=statusZauteosti,
                vlasnik=vlasnik,
                tip_vozila=tip_vozila,
                reg_broj=reg_broj,
                vreme_ulaska=vreme_ulaska                
                };
           
         
            he.ParkingMestoes.Add(pm);
            he.SaveChanges();
            return true;
        }

        void IspisiSlobodnaMesta()
        {
            lblSlobodnaMesta.Content = "SLOBODNA MESTA: "+ slobodnoMesta.ToString();           
        }

        int slobodnoMesta=5;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Forme.LoginPodaci wpfPodaci = new Forme.LoginPodaci();          


            bool? rezultatDialoga = wpfPodaci.ShowDialog();
            if (rezultatDialoga is true)
            {
                if (Class1.txtKorisnickoIme == Class1.adminKIme && Class1.txtSifra==Class1.adminPass)
                {
                    Forme.wpfAdmin aa = new Forme.wpfAdmin();
                    aa.Show();
                }
                else
                {
                    MessageBox.Show("pogresna lozinka.. vise srece drugi put :)");
                }
            }
          
        }


        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Forme.wpfRegistracija ff = new Forme.wpfRegistracija();
            ff.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          haris_bazaEntities Ha=new haris_bazaEntities();
          foreach(ParkingMesto pm in Ha.ParkingMestoes)
          {

                Label dd=(Label) this.FindName("lbl"+pm.brojMesta);
               
                        if (pm.statusZauzetosti is true)
                        dd.Background = Brushes.Red; 
                        else
                        dd.Background = Brushes.Orange;

                dd.Foreground = Brushes.Black;
                dd.Content = nazikorisnika(pm.vlasnik, pm.tip_vozila, pm.reg_broj, DateTime.Parse(pm.vreme_ulaska.ToString()));
                slobodnoMesta -= 1;
                IspisiSlobodnaMesta();
              
          }
         
        }
    }
}
