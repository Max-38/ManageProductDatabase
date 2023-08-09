using DevExpress.Mvvm;
using ManageProductDatabase.NsApplication;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManageProductDatabase.App.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Native;
using ManageProductDatabase.App.Views;
using System.Windows;
using System.Windows.Controls;

namespace ManageProductDatabase.App.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal ItemsCount { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        private readonly ProductRepositoryHandler productRepositoryHandler;

        public MainWindowVM(ProductRepositoryHandler productRepositoryHandler)
        {
            this.productRepositoryHandler = productRepositoryHandler;

            Task.Run(GetListProducts);
        }

        private async Task GetListProducts()
        {
            List<Product> listOfProducts = await productRepositoryHandler.GetAll();
            Products = listOfProducts.ToObservableCollection();
        }

        public ICommand AddProduct => new RelayCommand(async obj =>
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.Owner = Application.Current.MainWindow;
            addProductWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addProductWindow.ShowDialog();
        });

        public ICommand Add => new RelayCommand(async obj =>
        {
            StackPanel stackPanel = obj as StackPanel;

            if (stackPanel.BindingGroup.CommitEdit())
            {
                await productRepositoryHandler.Add(Name, Price, ItemsCount);
                await GetListProducts();

                reset();
            }
            else
                MessageBox.Show("Данные введены неверно");
        });

        public ICommand UpdateProduct => new RelayCommand(async obj =>
        {
            Product product = obj as Product;

            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            ItemsCount = product.ItemsCount;

            UpdateProductWindow updateProductWindow = new UpdateProductWindow();
            updateProductWindow.Owner = Application.Current.MainWindow;
            updateProductWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            updateProductWindow.ShowDialog();            
        }, obj => obj != null);

        public ICommand Update => new RelayCommand(async obj =>
        { 
            StackPanel stackPanel = obj as StackPanel;
            if (stackPanel.BindingGroup.CommitEdit())
            {
                Product product = new Product(Id, Name, Price, ItemsCount);

                await productRepositoryHandler.Update(product);
                await GetListProducts();

                reset();
            }
            else
                MessageBox.Show("Данные введены неверно");
        });

        public ICommand DeleteProduct => new RelayCommand(async obj =>
        {
            Product product = obj as Product;
            await productRepositoryHandler.Delete(product.Id);
            Products.Remove(product);

        }, obj => obj != null);

        public ICommand Close => new RelayCommand(async obj =>
        {
            Window window = obj as Window;
            window.Close();
        });

        private void reset()
        {
            Name = string.Empty;
            Price = 0;
            ItemsCount = 0;
            Application.Current.Windows[2].Close();
        }
    }
}
