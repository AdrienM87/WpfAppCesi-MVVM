using System;
using System.Data;
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
using WpfAppCesi.ViewModel;

//TODO : appliquer MVVM
/*
    InotifiedPropertyChanged

    CRUD -> viewModel

    ICommand pour envoyer de la view au vm
 */

//TODO : retirer les colonnes inutiles des datagrid
//¤ replacer controles dans des grids pour groupbox

namespace WpfAppCesi
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum EnumGestionInterfaces
        {
            Hotel = 0,
            Chambre = 1,
            Client = 2,
            Reservation = 3
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadDatas();
        }

        private void LoadDatas()
        {
            try
            {
                this.HotelsDataGrid.ItemsSource = new HotelsViewModel().VMgetHotels();
                this.ChambresDataGrid.ItemsSource = new ChambresViewModel().VMgetChambres();
                this.ClientsDataGrid.ItemsSource = new ClientsViewModel().VMgetClients();
                this.ReservationDataGrid.ItemsSource = new ReservationsViewModel().VMgetReservations();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region Gestion des hotels

        private void ViderComposantsHotels()
        {
            try
            {
                this.LbIdHotel.Content = "";
                this.TbNomHotel.Text = "";
                this.TbCapacite.Text = "";
                this.TbLocalisation.Text = "";
                this.TbPays.Text = "";
                this.HotelsDataGrid.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ActiverDesactiverControlesHotels(bool choix)
        {
            try
            {
                if (!choix)
                {
                    ViderComposantsHotels();
                }
                this.TbNomHotel.IsEnabled = choix;
                this.TbCapacite.IsEnabled = choix;
                this.TbLocalisation.IsEnabled = choix;
                this.TbPays.IsEnabled = choix;
                this.BtValiderHotel.IsEnabled = choix;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtAjouterHotel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtAjouterHotel.IsEnabled = false;
                this.BtSupprimerHotel.IsEnabled = false;

                ViderComposantsHotels();
                ActiverDesactiverControlesHotels(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtSupprimerHotel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtSupprimerHotel.IsEnabled = false;

                if (this.HotelsDataGrid.SelectedItem == null)
                {
                    return;
                }
                bool result = new HotelsViewModel().VMsupprimerHotel((HotelsSet)this.HotelsDataGrid.SelectedItem);
                if (result)
                {
                    LoadHotels();
                    MessageBox.Show("Suppression effectuée !");
                }
                else
                {
                    MessageBox.Show("Erreur de suppression.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtValiderHotel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idHotel = 0;

                if ((string)this.LbIdHotel.Content != "" && this.LbIdHotel.Content != null)
                {
                    idHotel = Convert.ToInt32(this.LbIdHotel.Content);
                }

                bool result = new HotelsViewModel().VMvaliderHotel(

                                idHotel,
                                this.TbNomHotel.Text,
                                Convert.ToInt32(this.TbCapacite.Text),
                                this.TbLocalisation.Text,
                                this.TbPays.Text
                );
                if (result)
                {
                    LoadHotels();
                    ActiverDesactiverControlesHotels(false);
                    MessageBox.Show("Enregistré !");
                }
                else
                {
                    MessageBox.Show("Enregistrement échoué.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void HotelsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.HotelsDataGrid.SelectedItem == null)
                {
                    return;
                }
                HotelsSet hotel = (HotelsSet)HotelsDataGrid.SelectedItem;
                if (hotel == null)
                {
                    return;
                }

                this.BtAjouterHotel.IsEnabled = true;
                this.BtSupprimerHotel.IsEnabled = true;

                this.LbIdHotel.Content = hotel.Id.ToString();
                this.TbNomHotel.Text = hotel.Nom;
                this.TbCapacite.Text = hotel.Capacite.ToString();
                this.TbLocalisation.Text = hotel.Localisation;
                this.TbPays.Text = hotel.Pays;

                ActiverDesactiverControlesHotels(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                this.CbHotels_TabChambres.SelectedIndex = -1;
                this.LbNomHotel_TabChambres.Content = "";
                this.ChambresDataGrid.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }

        private void BtAjouterChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtAjouterChambre.IsEnabled = false;
                this.BtSupprimerChambre.IsEnabled = false;

                ViderComposantsChambres();
                ActiverDesactiverControlesChambres(true);

                ChargementComboHotels(this.CbHotels_TabChambres);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtSupprimerChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtSupprimerChambre.IsEnabled = false;

                if (this.ChambresDataGrid.SelectedItem == null)
                {
                    return;
                }
                //bool result = ((HotelsSet)this.ChambresDataGrid.SelectedItem).SupprimerHotel();
                //if (result)
                //{
                //    LoadHotels();
                //    MessageBox.Show("Suppression effectuée !");
                //}
                //else
                //{
                //    MessageBox.Show("Erreur de suppression.");
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                    chambre.KeyHotel = Convert.ToInt32(this.CbHotels_TabChambres.SelectedValue);

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
                if (CbHotels_TabChambres.SelectedValue == null)
                {
                    return;
                }

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
                throw new Exception(ex.Message);
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
                this.CbHotels_TabChambres.SelectedItem = chambre.KeyHotel;

                ActiverDesactiverControlesChambres(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtReserverChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.TabGestion.SelectedIndex = (int)EnumGestionInterfaces.Reservation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Gestion des clients

        private void ViderComposantsClients()
        {
            try
            {
                this.LbIdClient.Content = "";
                this.TbNomClient.Text = "";
                this.TbPrenomClient.Text = "";
                this.DtDateNaissance.SelectedDate = null;
                this.ClientsDataGrid.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ActiverDesactiverControlesClients(bool choix)
        {
            try
            {
                if (!choix)
                {
                    ViderComposantsClients();
                }
                this.TbNomClient.IsEnabled = choix;
                this.TbPrenomClient.IsEnabled = choix;
                this.DtDateNaissance.IsEnabled = choix;
                this.BtValiderClient.IsEnabled = choix;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtAjouterClient.IsEnabled = false;
                this.BtSupprimerClient.IsEnabled = false;

                ViderComposantsClients();
                ActiverDesactiverControlesClients(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtSupprimerClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtSupprimerHotel.IsEnabled = false;

                if (this.HotelsDataGrid.SelectedItem == null)
                {
                    return;
                }
                //bool result = ((HotelsSet)this.HotelsDataGrid.SelectedItem).SupprimerHotel();
                //if (result)
                //{
                //    LoadHotels();
                //    MessageBox.Show("Suppression effectuée !");
                //}
                //else
                //{
                //    MessageBox.Show("Erreur de suppression.");
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtValiderClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    bool isNewClient;
                    ClientsSet client;

                    if ((string)this.LbIdClient.Content != "" && this.LbIdClient.Content != null)
                    {
                        isNewClient = false;
                        int idClient = Convert.ToInt32(this.LbIdClient.Content);

                        var resultQuery = (from clients in db.ClientsSet
                                           where clients.Id == idClient
                                           select clients);

                        if (resultQuery.Any())
                        {
                            client = resultQuery.First();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        isNewClient = true;
                        client = new ClientsSet();
                    }
                    client.Nom = this.TbNomClient.Text;
                    client.Prenom = this.TbPrenomClient.Text;
                    client.DateNaissance = (DateTime)this.DtDateNaissance.SelectedDate;

                    if (isNewClient)
                    {
                        db.ClientsSet.Add(client);
                    }
                    db.SaveChanges();
                }
                MessageBox.Show("Enregistré !");

                LoadClients();
                ActiverDesactiverControlesClients(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BtReserverChambre_TabClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.TabGestion.SelectedIndex = (int)EnumGestionInterfaces.Reservation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.ClientsDataGrid.SelectedItem == null)
                {
                    return;
                }
                ClientsSet client = (ClientsSet)ClientsDataGrid.SelectedItem;
                if (client == null)
                {
                    return;
                }

                this.BtAjouterClient.IsEnabled = true;
                this.BtSupprimerClient.IsEnabled = true;

                this.LbIdClient.Content = client.Id.ToString();
                this.TbNomClient.Text = client.Nom;
                this.TbPrenomClient.Text = client.Prenom.ToString();
                this.DtDateNaissance.SelectedDate = client.DateNaissance;

                ActiverDesactiverControlesClients(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Gestion des reservations

        private void ViderComposantsReservations()
        {
            try
            {
                this.LbIdReservation.Content = "";
                this.LbNomHotel_TabReservations.Content = "";
                this.DtDebutReservation.SelectedDate = null;
                this.DtFinReservation.SelectedDate = null;
                this.CbClient.SelectedIndex = -1;
                this.CbChambres.SelectedIndex = -1;
                this.ReservationDataGrid.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ActiverDesactiverControlesReservations(bool choix)
        {
            try
            {
                if (!choix)
                {
                    ViderComposantsReservations();
                }
                this.DtDebutReservation.IsEnabled = choix;
                this.DtFinReservation.IsEnabled = choix;
                this.CbClient.IsEnabled = choix;
                this.CbHotels_TabReservations.IsEnabled = choix;
                this.BtValiderReservation.IsEnabled = choix;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtNouvelleReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtNouvelleReservation.IsEnabled = false;
                this.BtSupprimerReservation.IsEnabled = false;

                ViderComposantsReservations();
                ActiverDesactiverControlesReservations(true);

                ChargementComboHotels(this.CbHotels_TabReservations);
                ChargementComboClients(this.CbClient);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtSupprimerReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BtSupprimerHotel.IsEnabled = false;

                if (this.HotelsDataGrid.SelectedItem == null)
                {
                    return;
                }
                //bool result = ((HotelsSet)this.HotelsDataGrid.SelectedItem).SupprimerHotel();
                //if (result)
                //{
                //    LoadHotels();
                //    MessageBox.Show("Suppression effectuée !");
                //}
                //else
                //{
                //    MessageBox.Show("Erreur de suppression.");
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                    reservation.DateDebut = (DateTime)this.DtDebutReservation.SelectedDate;
                    reservation.DateFin = (DateTime)this.DtFinReservation.SelectedDate;
                    reservation.KeyClient = Convert.ToInt32(this.CbClient.SelectedValue);
                    reservation.KeyChambre = Convert.ToInt32(this.CbChambres.SelectedValue);

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
                if (CbHotels_TabReservations.SelectedValue == null)
                {
                    return;
                }

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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                this.DtDebutReservation.SelectedDate = reservation.DateDebut;
                this.DtFinReservation.SelectedDate = reservation.DateFin;
                this.CbClient.SelectedItem = reservation.KeyClient;
                this.CbChambres.SelectedItem = reservation.KeyChambre;

                using (var db = new ModelBooking())
                {
                    var resultQuery = (from chambre in db.ChambresSet
                                       where chambre.Id == reservation.KeyChambre
                                       select chambre.KeyHotel);

                    if (resultQuery.Any())
                    {
                        this.CbHotels_TabReservations.SelectedItem = resultQuery.First();
                    }
                }
                ActiverDesactiverControlesReservations(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtNouveauClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.TabGestion.SelectedIndex = (int)EnumGestionInterfaces.Client;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }

        private void BtRecharger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadDatas();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
