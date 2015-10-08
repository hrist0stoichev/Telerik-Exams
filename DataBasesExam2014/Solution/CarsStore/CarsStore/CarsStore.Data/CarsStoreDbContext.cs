namespace CarsStore.Data
{
    using System.Data.Entity;

    using CarsStore.Models;

    public class CarsStoreDbContext : DbContext
    {
        public CarsStoreDbContext() : base("CarsStoreConnection")
        {
        }

        public IDbSet<Car> Cars { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Dealer> Dealers { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

        public IDbSet<Transmission> Transmissions { get; set; }
    }
}
