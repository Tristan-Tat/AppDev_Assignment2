namespace AppDev_Assignment2.Model
{
    public class Response
    {
        /*
       * the purpose of this class is to have a structure of the response we are going to get from the remote server
       */

        public int statusCode { get; set; }
        public string message { get; set; }
        public Product product { get; set; }
        public List<Product> products { get; set; }
    }
}
