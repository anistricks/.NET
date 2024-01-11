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

namespace GestionnairesDeTaches
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GestionsTaches gestionsTaches = new GestionsTaches();
        public MainWindow()
        {
            InitializeComponent();
           
            dataGridTaches.DataContext = gestionsTaches.TachesList;
            IList<int> priorités = new List<int>();
            for (int i = 1; i <= 5; i++)
            {
                priorités.Add(i);
            }
            PrioriteValeur.DataContext = priorités;

        }

        private void dataGridTaches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridTaches.SelectedItem is Tache selectedTache) { 
            Tache tache =(Tache)dataGridTaches.SelectedItem;
            DescriptionValeur.Text = tache.Description;
            PrioriteValeur.SelectedItem = tache.Priorité;
            DateValeur.SelectedDate = tache.Date;
            TermineValeur.IsChecked = tache.Termine;
            }
            else
            {
                // Si aucune tâche n'est sélectionnée, réinitialisez les champs.
                DescriptionValeur.Text = "";
                PrioriteValeur.SelectedItem = null;
                DateValeur.SelectedDate = null;
                TermineValeur.IsChecked = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tache nouvelleTache = new Tache(DescriptionValeur.Text);
            nouvelleTache.Priorité = (int)PrioriteValeur.SelectedItem;
            nouvelleTache.Date = (DateTime)DateValeur.SelectedDate;
            nouvelleTache.Termine = TermineValeur.IsChecked.Value;
            gestionsTaches.TachesList.Add(nouvelleTache);

            dataGridTaches.Items.Refresh();


        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Tache modifTache = (Tache)dataGridTaches.SelectedItem;
            gestionsTaches.TachesList.Remove(modifTache);
            dataGridTaches.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dataGridTaches.SelectedItem = null;
            DescriptionValeur.Text = "";
            PrioriteValeur.SelectedItem = 3;

        }
    }
}
