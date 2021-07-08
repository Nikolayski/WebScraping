namespace WebScraping
{
    using HtmlAgilityPack;

    using System;
    using System.Web;
    using System.Text;
    using System.Globalization;
    using System.Linq;

    public class HtmlParser
    {
        public HtmlParser(){}

        public  string GetProductName(HtmlNode div)
        {
            string productName = div.Descendants("img")
                                 .FirstOrDefault()
                                 .Attributes["alt"]
                                 .Value;
            return HttpUtility.HtmlDecode(productName);
        }

        public  decimal GetPrice(HtmlNode div)
        {
            string priceString = div.Descendants("span")
                            .FirstOrDefault(x => x.Attributes.Contains("style"))
                            .InnerText;
            priceString = priceString.Replace("$", string.Empty);
            decimal price = decimal.Parse(priceString, CultureInfo.InvariantCulture);
            return price;
        }

        public  decimal GetRating(HtmlNode div)
        {
            int maxRatingValue = 5;
            string ratingString = div.Attributes["rating"].Value;
            decimal rating = decimal.Parse(ratingString, CultureInfo.InvariantCulture);
            if (rating <= maxRatingValue) return rating;
            rating = (rating / 10) * maxRatingValue;
            return Math.Round(rating, 1, MidpointRounding.AwayFromZero);
        }

        public  Product CreateProduct(string productName, decimal price, decimal rating)
        {
            return new Product
            {
                ProductName = productName,
                Price = price,
                Rating = rating
            };
        }

        public  void PrintProduct(Product product)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Product name: {product.ProductName}");
            sb.AppendLine($"Price: {product.Price}");
            sb.AppendLine($"Rating: {product.Rating}");
            sb.AppendLine("\n");
            Console.WriteLine(sb.ToString());
        }
    }
}
