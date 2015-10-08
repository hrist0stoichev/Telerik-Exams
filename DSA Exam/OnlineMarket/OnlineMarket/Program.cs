namespace OnlineMarket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wintellect.PowerCollections;

    class Program
    {
        private static MultiDictionary<string, Product> items = new MultiDictionary<string, Product>(true);
        private static HashSet<string> names = new HashSet<string>();

        static void Main(string[] args)
        {
            var currentLine = Console.ReadLine();
            while (currentLine != "end")
            {
                var lineParameters = currentLine.Split(' ');
                if (lineParameters[0] == "add")
                {
                    Add(lineParameters[1], double.Parse(lineParameters[2]), lineParameters[3]);
                }
                else if (lineParameters[0] == "filter")
                {
                    switch (lineParameters[2])
                    {
                        case "type":
                            FilterByType(lineParameters[3]);
                            break;
                        case "price":
                            if (lineParameters[3] == "from")
                            {
                                if (currentLine.Contains("to"))
                                {
                                    FilterByPriceFromTo(lineParameters[4], lineParameters[6]);
                                }
                                else
                                {
                                    FilterByPriceFrom(lineParameters[4]);
                                }
                            }
                            else if (lineParameters[3] == "to")
                            {
                                FilterByPriceTo(lineParameters[4]);
                            }
                            break;
                    }
                }

                currentLine = Console.ReadLine();
            }
        }

        private static void FilterByPriceFromTo(string p1, string p2)
        {
            var allProducts = new List<Product>();
            foreach (var item in items.Values)
            {
                allProducts.Add(item);
            }

            var sorted = allProducts.Where(a => a.Price >= double.Parse(p1)).Where(a => a.Price <= double.Parse(p2)).OrderBy(a => a.Price).ThenBy(a => a.Name).ThenBy(a => a.ProductType).Take(10).ToList();
            Console.WriteLine("Ok: " + string.Join(", ", sorted));
        }

        private static void FilterByPriceFrom(string p)
        {
            var allProducts = new List<Product>();
            foreach (var item in items.Values)
            {
                allProducts.Add(item);
            }

            var sorted = allProducts.Where(a => a.Price >= double.Parse(p)).OrderBy(a => a.Price).ThenBy(a => a.Name).ThenBy(a => a.ProductType).Take(10).ToList();
            Console.WriteLine("Ok: " + string.Join(", ", sorted));
        }

        private static void FilterByPriceTo(string p)
        {
            var allProducts = new List<Product>();
            foreach (var item in items.Values)
            {
                allProducts.Add(item);
            }

            var sorted = allProducts.Where(a => a.Price <= double.Parse(p)).OrderBy(a => a.Price).ThenBy(a => a.Name).ThenBy(a => a.ProductType).Take(10).ToList();
            Console.WriteLine("Ok: " + string.Join(", ", sorted));
        }

        private static void FilterByType(string type)
        {
            if (!items.ContainsKey(type))
            {
                Console.WriteLine("Error: Type {0} does not exists", type);
                return;
            }

            var products = items[type];
            var sorted = products.OrderBy(p => p.Price).ThenBy(p => p.Name).Take(10).ToList();


            Console.WriteLine("Ok: " + string.Join(", ", sorted));
        }

        static void Add(string productName, double productPrice, string productType)
        {
            if (names.Contains(productName))
            {
                Console.WriteLine("Error: Product {0} already exists", productName);
            }
            else
            {
                names.Add(productName);
                items.Add(productType, new Product(productName, productPrice, productType));
                Console.WriteLine("Ok: Product {0} added successfully", productName);
            }
        }
    }

    class Product
    {
        public Product(string name, double price, string productType)
        {
            this.Name = name;
            this.Price = price;
            this.ProductType = productType;
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public string ProductType { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Name);
            sb.Append("(");
            sb.Append(this.Price);
            sb.Append(")");

            return sb.ToString();
        }
    }
}
