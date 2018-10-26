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

//TODO : appliquer MVVM
/*
    InotifiedPropertyChanged

    CRUD -> viewModel

    ICommand pour envoyer de la view au vm
 */

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

        #region Load des datas

        private void LoadDatas()
        {
            LoadHotels();
            LoadChambres();
            //LoadClients();
            LoadReservations();
        }

        private void LoadChambres()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from chambres in db.ChambresSet select chambres);

                    if (resultQuery.Any())
                    {
                        HashSet<ChambresSet> SetChambres = resultQuery.ToHashSet();
                        this.ChambresDataGrid.ItemsSource = SetChambres;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void LoadReservations()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from reservations in db.ReservationsSet select reservations);

                    if (resultQuery.Any())
                    {
                        HashSet<ReservationSet> SetReservations = resultQuery.ToHashSet();
                        this.ReservationDataGrid.ItemsSource = SetReservations;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void LoadHotels()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from hotels in db.HotelsSet select hotels);

                    if (resultQuery.Any())
                    {
                        HashSet<HotelsSet> SetHotels = resultQuery.ToHashSet();
                        this.HotelsDataGrid.ItemsSource = SetHotels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        #endregion

        #region Commun

        private void ChargementComboHotels(ComboBox combo)
        {
            try
            {
                combo.Items.Clear();

                using (var db = new ModelBooking())
                {
                    var resultQuery = (from hotels in db.HotelsSet select hotels);

                    if (resultQuery.Any())
                    {
                        HashSet<HotelsSet> SetComboHotels = resultQuery.ToHashSet();

                        foreach (HotelsSet hotel in SetComboHotels)
                        {
                            combo.Items.Add(hotel.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        #endregion

        #region Gestion des chambres

        private void ViderComposantsChambres()
        {
            try
            {
                this.LbIdChambre.Content = "";
                this.TbNomChambre.Text = "";
                this.TbNbLits.Text = "";
                this.RdHasClimO.IsChecked = false;
                this.RdHasClimN.IsChecked = false;
                //this.CbHotels.SelectedItem = null;
                this.LbNomHotel_TabChambres.Content = "";
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void ActiverDesactiverControlesChambres(bool choix)
        {
            try
            {
                if (!choix)
                {
                    ViderComposantsChambres();
                }
                this.BtValiderChambre.IsEnabled = choix;
                this.TbNomChambre.IsEnabled = choix;
                this.TbNbLits.IsEnabled = choix;
                this.RdHasClimO.IsEnabled = choix;
                this.RdHasClimN.IsEnabled = choix;
                this.CbHotels_TabChambres.IsEnabled = choix;
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

                ViderComposantsChambres();
                ActiverDesactiverControlesChambres(true);

                ChargementComboHotels(this.CbHotels_TabChambres);
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
                this.BtSupprimerChambre.IsEnabled = false;

                int idChambre;
                if (this.ChambresDataGrid.SelectedItem == null)
                {
                    return;
                }
                else
                {
                    idChambre = Convert.ToInt32(((ChambresSet)this.ChambresDataGrid.SelectedItem).Id);
                }

                using (var db = new ModelBooking())
                {
                    var resultQuery = (from chambres in db.ChambresSet
                                       where chambres.Id == idChambre
                                       select chambres);

                    if (resultQuery.Any())
                    {
                        db.ChambresSet.Remove(resultQuery.First());
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Suppression effectuée !");
                LoadChambres();
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
                if (CbHotels_TabChambres.SelectedValue == null)
                {
                    MessageBox.Show("Veuillez choisir un hotel");
                    return;
                }

                using (var db = new ModelBooking())
                {
                    bool isNewChambre;
                    ChambresSet chambre;

                    if ((string)this.LbIdChambre.Content != "" && this.LbIdChambre.Content != null)
                    {
                        isNewChambre = false;
                        int idChambre = Convert.ToInt32(this.LbIdChambre.Content);

                        var resultQuery = (from chambres in db.ChambresSet
                                           where chambres.Id == idChambre
                                           select chambres);

                        if (resultQuery.Any())
                        {
                            chambre = resultQuery.First();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        isNewChambre = true;
                        chambre = new ChambresSet();
                    }
                    chambre.Nom = this.TbNomChambre.Text;
                    chambre.Climatisation = (bool)this.RdHasClimO.IsChecked;
                    chambre.NbLits = Convert.ToInt32(this.TbNbLits.Text);
                    chambre.keyHotel = Convert.ToInt32(this.CbHotels_TabChambres.SelectedValue);

                    if (isNewChambre)
                    {
                        db.ChambresSet.Add(chambre);
                    }
                    db.SaveChanges();
                }
                MessageBox.Show("Enregistré !");

                LoadChambres();
                ActiverDesactiverControlesChambres(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CbHotels_TabChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from hotels in db.HotelsSet
                                       where hotels.Id == (int)this.CbHotels_TabChambres.SelectedValue
                                       select hotels.Nom);

                    if (resultQuery.Any())
                    {
                        this.LbNomHotel_TabChambres.Content = resultQuery.First();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void ChambresDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.ChambresDataGrid.SelectedItem == null)
                {
                    return;
                }
                ChambresSet chambre = (ChambresSet)ChambresDataGrid.SelectedItem;
                if (chambre == null)
                {
                    return;
                }

                ChargementComboHotels(this.CbHotels_TabChambres);

                this.BtAjouterChambre.IsEnabled = true;
                this.BtSupprimerChambre.IsEnabled = true;

                this.LbIdChambre.Content = chambre.Id.ToString();
                this.TbNomChambre.Text = chambre.Nom;
                this.TbNbLits.Text = chambre.NbLits.ToString();
                this.RdHasClimO.IsChecked = chambre.Climatisation;
                this.CbHotels_TabChambres.SelectedItem = chambre.keyHotel;

                ActiverDesactiverControlesChambres(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtReserverChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO : changement d'onglet
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        #endregion

        #region Gestion des reservations

        private void ViderComposantsReservations()
        {
            try
            {
                this.LbNomHotel_TabReservations.Content = "";
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void ActiverDesactiverControlesReservations(bool choix)
        {
            try
            {
                if (!choix)
                {
                    ViderComposantsChambres();
                }
                this.DtDebut.IsEnabled = choix;
                this.DtFin.IsEnabled = choix;
                this.CbClient.IsEnabled = choix;
                this.CbHotels_TabReservations.IsEnabled = choix;
                this.BtValiderReservation.IsEnabled = choix;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtNouvelleReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtNouvelleReservation.IsEnabled = false;

                ViderComposantsReservations();
                ActiverDesactiverControlesReservations(true);

                ChargementComboHotels(this.CbHotels_TabReservations);
                ChargementComboClients(this.CbClient);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtSupprimerReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtSupprimerReservation.IsEnabled = false;

                int idReservation;
                if (this.ReservationDataGrid.SelectedItem == null)
                {
                    return;
                }
                else
                {
                    idReservation = Convert.ToInt32(((ReservationSet)this.ReservationDataGrid.SelectedItem).Id);
                }

                using (var db = new ModelBooking())
                {
                    var resultQuery = (from reservations in db.ReservationsSet
                                       where reservations.Id == idReservation
                                       select reservations);

                    if (resultQuery.Any())
                    {
                        db.ReservationsSet.Remove(resultQuery.First());
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Suppression effectuée !");
                LoadReservations();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        //TODO : pour les ajouts penser à l'objet en plus de la foreign key

        private void BtValiderReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CbChambres.SelectedValue == null)
                {
                    MessageBox.Show("Veuillez choisir un hotel puis une chambre");
                    return;
                }

                using (var db = new ModelBooking())
                {
                    bool isNewReservation;
                    ReservationSet reservation;

                    if ((string)this.LbIdReservation.Content != "" && this.LbIdReservation.Content != null)
                    {
                        isNewReservation = false;
                        int idReservation = Convert.ToInt32(this.LbIdReservation.Content);

                        var resultQuery = (from reservations in db.ReservationsSet
                                           where reservations.Id == idReservation
                                           select reservations);

                        if (resultQuery.Any())
                        {
                            reservation = resultQuery.First();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        isNewReservation = true;
                        reservation = new ReservationSet();
                    }
                    reservation.dateDebut = (DateTime)this.DtDebut.SelectedDate;
                    reservation.dateFin = (DateTime)this.DtFin.SelectedDate;
                    reservation.keyClient = Convert.ToInt32(this.CbClient.SelectedValue);
                    reservation.keyChambre = Convert.ToInt32(this.CbChambres.SelectedValue);

                    if (isNewReservation)
                    {
                        db.ReservationsSet.Add(reservation);
                    }
                    db.SaveChanges();
                }
                MessageBox.Show("Enregistré !");

                LoadReservations();
                ActiverDesactiverControlesReservations(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CbHotels_TabReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from hotels in db.HotelsSet
                                       where hotels.Id == (int)this.CbHotels_TabReservations.SelectedValue
                                       select hotels.Nom);

                    if (resultQuery.Any())
                    {
                        this.LbNomHotel_TabReservations.Content = resultQuery.First();

                        ChargementComboChambres(this.CbChambres);
                        this.CbChambres.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void ChargementComboChambres(ComboBox combo)
        {
            try
            {
                combo.Items.Clear();

                using (var db = new ModelBooking())
                {
                    var resultQuery = (from chambres in db.ChambresSet select chambres);

                    if (resultQuery.Any())
                    {
                        HashSet<ChambresSet> SetComboChambres = resultQuery.ToHashSet();

                        foreach (ChambresSet chambre in SetComboChambres)
                        {
                            combo.Items.Add(chambre.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void ChargementComboClients(ComboBox combo)
        {
            try
            {
                combo.Items.Clear();

                using (var db = new ModelBooking())
                {
                    var resultQuery = (from clients in db.ClientsSet select clients);

                    if (resultQuery.Any())
                    {
                        HashSet<ClientsSet> SetComboClients = resultQuery.ToHashSet();

                        foreach (ClientsSet client in SetComboClients)
                        {
                            combo.Items.Add(client.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void ReservationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.ReservationDataGrid.SelectedItem == null)
                {
                    return;
                }
                ReservationSet reservation = (ReservationSet)ReservationDataGrid.SelectedItem;
                if (reservation == null)
                {
                    return;
                }

                ChargementComboHotels(this.CbHotels_TabReservations);
                ChargementComboClients(this.CbClient);

                this.BtNouvelleReservation.IsEnabled = true;
                this.BtSupprimerReservation.IsEnabled = true;

                this.LbIdReservation.Content = reservation.Id.ToString();
                this.DtDebut.SelectedDate = reservation.dateDebut;
                this.DtFin.SelectedDate = reservation.dateFin;
                this.CbClient.SelectedItem = reservation.keyClient;
                this.CbChambres.SelectedItem = reservation.keyChambre;

                using (var db = new ModelBooking())
                {
                    var resultQuery = (from chambre in db.ChambresSet
                                       where chambre.Id == reservation.keyChambre
                                       select chambre.keyHotel);

                    if (resultQuery.Any())
                    {
                        this.CbHotels_TabReservations.SelectedItem = resultQuery.First();
                    }
                }
                ActiverDesactiverControlesReservations(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        //TODO : vider les combos dans les fonctions dédiées

        private void BtNouveauClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO : changement d'onglet
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        #endregion

        #region Gestion des hotels



        #endregion


        private void BtRecharger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadDatas();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtQuitter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

        private void BtAjouterHotel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtSupprimerHotel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtValiderHotel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
