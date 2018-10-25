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
                using (var datas = new ModelBooking())
                {
                    HashSet<ChambresSet> SetChambres = (from chambres in datas.ChambresSet select chambres).ToHashSet();
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
        private void btAjouterChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var datas = new ModelBooking())
                {
                    using (var db = new ModelBooking())
                    {
                        ChambresSet chambre = new ChambresSet();
                        chambre.Nom = this.TbNomChambre.Text;
                        chambre.Climatisation = (bool)this.RdHasClimO.IsChecked;
                        chambre.NbLits = Convert.ToInt32(this.TbNbLits.Text);
                        chambre.HotelsSetId = Convert.ToInt32(this.CbHotels.SelectedValue);

                        db.ChambresSet.Add(chambre);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void btModifierChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void btSupprimerChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }



        #endregion

        private void btReserverChambre_Click(object sender, RoutedEventArgs e)
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
