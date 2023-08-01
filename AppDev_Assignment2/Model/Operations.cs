using Npgsql;
using System.Xml.Linq;

namespace AppDev_Assignment2.Model
{
    public class Operations
    {
        // class for CRUD operations

        // INSERT
        public static Response createProduct(NpgsqlConnection con, Product product)
        {
            Response response = new Response();
            try
            {
                string Insert = "insert into products (name, id, amount, price) values (@name, @id, @weight, @price);";
                NpgsqlCommand cmd = new NpgsqlCommand(Insert, con);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@weight", product.Weight);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@id", product.Id);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    response.statusCode = 200;
                    response.message = "Successfully inserted product.";
                    response.product = product;
                    response.products = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.message = "Failed to insert product.";
                    response.product = null;
                    response.products = null;
                }
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                Console.Write(ex.Message);
            }
            return response;
        }

        // DELETE
        public static Response deleteProduct(NpgsqlConnection con, int id)
        {
            Response response = new Response();
            try
            {
                string Delete = "delete from products where id = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(Delete, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open() ;
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    response.statusCode = 200;
                    response.message = "Successfully deleted product.";
                    response.product = null;
                    response.products = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.message = "Failed to delete product.";
                    response.product = null;
                    response.products = null;
                }
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                Console.Write(ex.Message);
            }
            return response;
        }

        // UPDATE
        public static Response updateProduct(NpgsqlConnection con, Product product)
        {
            Response response = new Response();
            try
            {
                string Update = "update products set name = @name, amount = @weight, price = @price where id = @id;";
                NpgsqlCommand cmd = new NpgsqlCommand(Update, con);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@weight", product.Weight);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@id", product.Id);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    response.statusCode = 200;
                    response.message = "Successfully updated product.";
                    response.product = product;
                    response.products = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.message = "Failed to update product.";
                    response.product = null;
                    response.products = null;
                }
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                Console.Write(ex.Message);
            }
            return response;
        }

        // SELECT
        public static Response getProducts(NpgsqlConnection con)
        {

            Response response = new Response();
            try
            {
                string Query = "select * from products;";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

                con.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();

                List<Product> products = new List<Product>();
                while (reader.Read())
                {
                    products.Add(new Product(reader.GetInt32(1), reader.GetString(0), reader.GetInt32(2), reader.GetDecimal(3)));
                }

                response.statusCode = 200;
                response.message = "Successfully obtained products.";
                response.product = null;
                response.products = products;

            con.Close() ;
            }
            catch (NpgsqlException ex)
            {
                Console.Write(ex.Message);
            }
            return response;
        }
    }
}
