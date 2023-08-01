using Assignment2_Admin.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment2_Admin.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

       HttpClient client = new HttpClient();


            // GET "Api/GetAllProducts"

            // POST "Api/AddProduct"
            // PUT "Api/UpdateProduct"
            // DELETE "Api/DeleteProduct"
        public AdminWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7159/Api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            UpdateListOfProducts();
            InitializeComponent();
        }

        public async void UpdateListOfProducts()
        {
            var res = await client.GetStringAsync("GetAllProducts");
            Response response = JsonConvert.DeserializeObject<Response>(res);

            ListOfProducts.ItemsSource = new ObservableCollection<Model.Product>(response.products); 
        }

        private async void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListOfProducts.SelectedItem != null)
            try 
            {
                    Product product = (Product) ListOfProducts.SelectedItem;
                    var res = await client.DeleteAsync("DeleteProduct/" + product.Id);
                    MessageBox.Show("Successfully deleted product#" + product.Id);
                    UpdateListOfProducts();
            }
            catch { }
        }

        private async void UpdateProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListOfProducts.SelectedItem != null)
            try
            {
                Product selectedProduct = (Product) ListOfProducts.SelectedItem;
                int id = selectedProduct.Id;
                String name = NameBoxUpdate.Text;
                int weight = int.Parse(WeightBoxUpdate.Text);
                decimal price = decimal.Parse(PriceBoxUpdate.Text);

                var res = await client.PutAsJsonAsync("UpdateProduct", new Product(id, name, weight, price));
                MessageBox.Show("Successfully updated product#" + selectedProduct.Id);
                UpdateListOfProducts();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(IdBoxCreate.Text);
                String name = NameBoxCreate.Text;
                int weight = int.Parse(WeightBoxCreate.Text);
                decimal price = decimal.Parse(PriceBoxCreate.Text);

                var res = await client.PostAsJsonAsync("AddProduct", new Product(id, name, weight, price));
                MessageBox.Show("Successfully added product#" + id);
                UpdateListOfProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
