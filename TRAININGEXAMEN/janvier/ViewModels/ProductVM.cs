using janvier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.ViewModels;

namespace janvier.ViewModels
{
    public class ProductVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<ProductModel> _productsList;
        private ProductModel _selectedProduct;
        private DelegateCommand _removeCommand;
        private ObservableCollection<AffichageModel> _affichageList;

      

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<ProductModel> ProductsList
        {
            get { return _productsList ?? loadProductsList(); }
        }

        private ObservableCollection<ProductModel> loadProductsList()
        {
            ObservableCollection<ProductModel> localCollection=new ObservableCollection<ProductModel>();
            foreach(var p in dc.Products.Where(p => p.Discontinued == false))
            {
                localCollection.Add(new ProductModel(p));
            }
            return localCollection;
        }

        public DelegateCommand RemoveCommand
        {
            get { return _removeCommand = _removeCommand ?? new DelegateCommand(RemoveProduct); }
        }

        private void RemoveProduct()
        {
            var productToUpdate = _selectedProduct;
            if(productToUpdate != null)
            {
                SelectedProduct.Product.Discontinued = true;
                //attention sauvegarder le changement apres l'avoir mis a true car c'est comme sa qu'il met la liste update
                dc.SaveChanges();
                //attention il faut l'enlever de la liste monsieur !!!!
                ProductsList.Remove(productToUpdate);
                OnPropertyChanged("ProductsList");
              


            }
        }
        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }

        public ObservableCollection<AffichageModel> AffichageList
        {
            get { return _affichageList ?? loadAffichangeList(); }
        }

        private ObservableCollection<AffichageModel> loadAffichangeList()
        {
            ObservableCollection<AffichageModel> localCollection = new ObservableCollection<AffichageModel>();

            var pays=dc.OrderDetails.Select(od=>od.Product.Supplier.Country).Distinct().ToList();
            foreach(var pay in pays)
            {
                var count = dc.OrderDetails.Where(od => od.Product.Supplier.Country == pay)
                    .Select(od => od.ProductId)
                    .Distinct().Count();

                localCollection.Add(new AffichageModel(pay, count));

            }
            var sortedCollection = new ObservableCollection<AffichageModel>(localCollection.OrderByDescending(am=>am.Count));
            //localCollection.OrderByDescending(am => am.Count);



            return sortedCollection;
        }
    }
}
