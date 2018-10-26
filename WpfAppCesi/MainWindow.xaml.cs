using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppCesi
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDatas();
        }

        private void LoadDatas()
        {
            //LoadHotels();
            LoadChambres();
            //LoadClients();
            //LoadReservations();
        }

        #region load des datas
        private void LoadChambres()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    HashSet<ChambresSet> SetChambres = (from chambres in db.ChambresSet select chambres).ToHashSet();
                    ChambresDataGrid.ItemsSource = SetChambres;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }
        #endregion

        #region gestion chambres
        private void BtAjouterChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtValiderChambre.IsEnabled = true;
                this.TbNomChambre.IsEnabled = true;
                this.TbNbLits.IsEnabled = true;
                this.RdHasClimO.IsEnabled = true;
                this.RdHasClimN.IsEnabled = true;
                this.CbHotels.IsEnabled = true;

                using (var db = new ModelBooking())
                {
                    HashSet<HotelsSet> SetComboHotels = (from hotels in db.HotelsSet select hotels).ToHashSet();
                    
                    foreach(HotelsSet hotel in SetComboHotels)
                    {
                        this.CbHotels.Items.Add(hotel.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtModifierChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtSupprimerChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtValiderChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    ChambresSet chambre = new ChambresSet();
                    chambre.Nom = this.TbNomChambre.Text;
                    chambre.Climatisation = (bool)this.RdHasClimO.IsChecked;
                    chambre.NbLits = Convert.ToInt32(this.TbNbLits.Text);
                    chambre.keyHotel = Convert.ToInt32(this.CbHotels.SelectedValue);

                    db.ChambresSet.Add(chambre);
                    db.SaveChanges();
                }
                MessageBox.Show("Chambre ajoutée !");
                LoadChambres();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void CbHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    this.LbNomHotel.Content = (from hotels in db.HotelsSet
                                                where hotels.Id == (int)this.CbHotels.SelectedValue
                                                select hotels.Nom).First();                    
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        #endregion

        private void BtReserverChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }


    }
}
