﻿using System;
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

        #region Load des datas

        private void LoadDatas()
        {
            //LoadHotels();
            LoadChambres();
            //LoadClients();
            //LoadReservations();
        }

        private void LoadChambres()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    HashSet<ChambresSet> SetChambres = (from chambres in db.ChambresSet select chambres).ToHashSet();
                    this.ChambresDataGrid.ItemsSource = SetChambres;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }
        #endregion

        #region Gestion des chambres

        private void ViderComposants()
        {
            try
            {
                this.LbIdChambre.Content = "";
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

        private void ChargementComboHotel()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from hotels in db.HotelsSet select hotels);

                    if (resultQuery.Any())
                    {
                        HashSet<HotelsSet> SetComboHotels = resultQuery.ToHashSet();

                        foreach (HotelsSet hotel in SetComboHotels)
                        {
                            this.CbHotels.Items.Add(hotel.Id);
                        }
                    }
                }
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

                ChargementComboHotel();
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
            if (CbHotels.SelectedValue == null)
            {
                MessageBox.Show("Veuillez choisir un hotel");
                return;
            }
            try
            {
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
                    chambre.keyHotel = Convert.ToInt32(this.CbHotels.SelectedValue);

                    if (isNewChambre)
                    {
                        db.ChambresSet.Add(chambre);
                    }
                    db.SaveChanges();
                }
                MessageBox.Show("Enregistré !");

                LoadChambres();
                ActiverDesactiverControles(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CbHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from hotels in db.HotelsSet
                                       where hotels.Id == (int)this.CbHotels.SelectedValue
                                       select hotels.Nom);

                    if (resultQuery.Any())
                    {
                        this.LbNomHotel.Content = resultQuery.First();
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

                ChargementComboHotel();

                this.BtAjouterChambre.IsEnabled = true;

                this.LbIdChambre.Content = chambre.Id.ToString();
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

        private void BtReserverChambre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if ((string)this.LbIdChambre.Content != "" && this.LbIdChambre.Content != null)
                //{
                //    using (var db = new ModelBooking())
                //    {
                //        var resultQuery = (from hotels in db.HotelsSet
                //                           where hotels.Id == (int)this.CbHotels.SelectedValue
                //                           select hotels.);



                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.ToString());
            }
        }

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


    }
}
