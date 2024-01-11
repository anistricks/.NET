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
using examen_septembre.Models;
using examen_septembre.ViewsModel;
namespace examen_septembre
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NorthwindContext dc =new NorthwindContext();    
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ProductVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newProductName = ProductNameTextBox.Text;
            string newQuantityPerUnit = QuantityPerUnitTextBox.Text;

            if (listBoxProduct.SelectedItem != null)
            {
                ProductModel selectedProduct = (ProductModel)listBoxProduct.SelectedItem;
                
                selectedProduct.ProductName = newProductName;
                selectedProduct.QuantityPerUnit = newQuantityPerUnit;

               
                dc.SaveChanges();
                
               
            }

        }
    }
}
