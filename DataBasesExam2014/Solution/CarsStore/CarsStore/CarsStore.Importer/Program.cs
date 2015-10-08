namespace CarsStore.Importer
{
    using System.IO;
    using System.Linq;

    using CarsStore.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    
    public class Program
    {
        static void Main(string[] args)
        {
            // Using SQLEXPRESS

            var db = new CarsStoreDbContext();

            db.Transmissions.Add(new Models.Transmission() { Type = "first" });
            db.SaveChanges();
        }
    }
}
