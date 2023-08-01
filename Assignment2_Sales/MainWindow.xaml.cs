using Assignment2_Sales.Controller;
using Assignment2_Sales.Models;
using Newtonsoft.Json;
using NSwag.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

namespace Assignment2_Sales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();

        private Product selectedCartProduct;

        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7159/Api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            updateList();
        }

        public async void updateList()
        {
            var res = await client.GetStringAsync("GetAllProducts");
            Models.Response response = JsonConvert.DeserializeObject<Models.Response>(res);

            //ProductsSource = new ObservableCollection<Product>(Admin.getProducts());
            ListOfProducts.ItemsSource = new ObservableCollection<Models.Product>(response.products);
        }

        public void updateCart()
        {
            //CartSource = new ObservableDictionary<Product, int>(Sales.Cart);
            Cart.ItemsSource = new ObservableDictionary<Models.Product, int>(Sales.Cart);
        }

        public void updateSalesTotal()
        {
            SalesTotalLabel.Content = Controller.Sales.calcSales() + "$";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ListOfProducts.SelectedItem != null)
            {
                Models.Product product = (Models.Product) ListOfProducts.SelectedItem;
                Controller.Sales.addToCart(product);
                updateCart();
                updateSalesTotal();
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (selectedCartProduct != null)
            {
                Controller.Sales.removeFromCart(selectedCartProduct);
            }
            selectedCartProduct = null;
            updateCart();
            updateSalesTotal();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (selectedCartProduct != null)
            {
                Controller.Sales.decrementCartProductQuantity(selectedCartProduct);
            }
            updateCart();
            updateSalesTotal();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (selectedCartProduct != null)
            {
                Controller.Sales.incrementCartProductQuantity(selectedCartProduct);
            }
            updateCart();
            updateSalesTotal();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (Controller.Sales.Cart.Count > 0)
            {
                Controller.Sales.performSales();
                updateCart();
                updateSalesTotal();
                MessageBox.Show("Products sold.");
                updateList();
            }
        }

        private void ListOfProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cart.SelectedItem != null)
            {
                DictionaryEntry entry = (DictionaryEntry)Cart.SelectedItem;
                selectedCartProduct = (Product)entry.Key;
            }
        }
    }
}
