namespace WebScraping
{
    using HtmlAgilityPack;

    class StartUp
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load("index.html");
            HtmlNodeCollection divWrappers = doc.DocumentNode.SelectNodes("//div[@class='item']");
            var parser = new HtmlParser();
            if (divWrappers != null)
            {
                foreach (var div in divWrappers)
                {
                    string productName = parser.GetProductName(div);
                    decimal price = parser.GetPrice(div);
                    decimal rating = parser.GetRating(div);
                    Product product = parser.CreateProduct(productName, price, rating);
                    parser.PrintProduct(product);
                }
            }
        }
    }
}
