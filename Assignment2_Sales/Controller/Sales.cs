using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment2_Sales.Controller
{
    internal class Sales
    {
        public static Dictionary<Models.Product, int> Cart = new Dictionary<Models.Product, int>();

        public static void addToCart(Models.Product product)
        {
            if (product.Weight > 0 && !Cart.ContainsKey(product))
            {
                Cart.Add(product, 1);
            }
            // update cart view
        }

        public static void removeFromCart(Models.Product product)
        {
            Cart.Remove(product);
            // update cart view
        }

        public static void incrementCartProductQuantity(Models.Product product)
        {
            if (Cart[product] < product.Weight && Cart.ContainsKey(product))
            {
                Cart[product] += 1;
            }
            // update cart view
        }

        public static void decrementCartProductQuantity(Models.Product product)
        {
            if (Cart[product] > 0 && Cart.ContainsKey(product))
            {
                Cart[product] -= 1;
            }
            // update cart view
        }

        public static int getCartProductQuantity(Models.Product product)
        {
            return Cart[product];
        }

        public static decimal calcSales()
        {
            decimal sales = 0.0M;
            foreach (KeyValuePair<Models.Product, int> entry in Cart)
            {
                sales += entry.Value * entry.Key.Price;
            }
            return sales;
        }

        public static void performSales()
        {
            foreach (KeyValuePair<Models.Product, int> entry in Cart)
            {
                Models.Product product = entry.Key;
                int substracted = entry.Value;
                RESTApi.updateProduct(product, product.Weight - substracted);
            }
            Cart.Clear();
        }
    }
}
