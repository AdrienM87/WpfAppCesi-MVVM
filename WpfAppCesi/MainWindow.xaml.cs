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

        private void ViderComposants()
        {
            try
            {
                this.TbNomChambre.Text = "";
                this.TbNbLits.Text = "";
                this.RdHasClimO.IsChecked = false;
                this.RdHasClimN.IsChecked = false;
                //this.CbHotels.SelectedItem = null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void ActiverDesactiverControles(bool choix)
        {
            try
            {
                if (!choix)
                {
                    ViderComposants();
                }
                this.BtValiderChambre.IsEnabled = choix;
                this.TbNomChambre.IsEnabled = choix;
                this.TbNbLits.IsEnabled = choix;
                this.RdHasClimO.IsEnabled = choix;
                this.RdHasClimN.IsEnabled = choix;
                this.CbHotels.IsEnabled = choix;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtAjouterChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtAjouterChambre.IsEnabled = false;

                ViderComposants();
                ActiverDesactiverControles(true);

                using (var db = new ModelBooking())
                {
                    HashSet<HotelsSet> SetComboHotels = (from hotels in db.HotelsSet select hotels).ToHashSet();

                    foreach (HotelsSet hotel in SetComboHotels)
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
                ActiverDesactiverControles(false);
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

        //TODO : interdire la sélection multiple
        private void ChambresDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.BtAjouterChambre.IsEnabled = true;

                ChambresSet chambre = (ChambresSet)ChambresDataGrid.SelectedItem;

                this.TbNomChambre.Text = chambre.Nom;
                this.TbNbLits.Text = chambre.NbLits.ToString();
                this.RdHasClimO.IsChecked = chambre.Climatisation;
                this.CbHotels.SelectedItem = chambre.keyHotel;

                ActiverDesactiverControles(true);
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
