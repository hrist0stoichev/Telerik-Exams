namespace TheCompany.RandomDataGenerator
{
    using TheCompany.Data;

    public abstract class DataGenerator : IDataGenerator
    {
        private IRandomGenerator randomGenerator;
        private TheCompanyEntities db;
        private int countOfGeneratedObjects;
        
        public DataGenerator(IRandomGenerator randomGenerator, TheCompanyEntities theCompanyEntities, int countOfGeneratedObjects)
        {
            this.randomGenerator = randomGenerator;
            this.db = theCompanyEntities;
            this.countOfGeneratedObjects = countOfGeneratedObjects;
        }

        protected IRandomGenerator RandomGenerator
        {
            get { return this.randomGenerator; }
        }

        protected TheCompanyEntities Database
        {
            get { return this.db; }
        }

        protected int CountOfGeneratedObjects
        {
            get { return this.countOfGeneratedObjects; }
        }

        public abstract void Generate();
    }
}
