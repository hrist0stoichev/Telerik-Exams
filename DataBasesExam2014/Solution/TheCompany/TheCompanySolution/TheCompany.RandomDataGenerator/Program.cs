namespace TheCompany.RandomDataGenerator
{
    using System.Collections.Generic;
    
    using TheCompany.Data;

    public class Program
    {
        private const int DepartmentsGeneratorCount = 100;
        private const int EmployeesGeneratorCount = 5000;
        private const int ProjectsGeneratorCount = 1000;
        private const int ReportsGeneratorCount = 250000;
        private const int EmployeesProjectsGeneratorCount = 1000;

        static void Main(string[] args)
        {
            var randomGenerator = RandomGenerator.Instance;
            var db = new TheCompanyEntities();
            db.Configuration.AutoDetectChangesEnabled = false;

            var listOfGenerators = new List<IDataGenerator>
            {
                new DepartmentDataGenerator(randomGenerator, db, DepartmentsGeneratorCount),
                new ProjectDataGenerator(randomGenerator, db, ProjectsGeneratorCount),
                new EmployeeDataGenerator(randomGenerator, db, EmployeesGeneratorCount),
                new ReportDataGenerator(randomGenerator, db, ReportsGeneratorCount),
                new EmployeeProjectDataGenerator(randomGenerator, db, EmployeesProjectsGeneratorCount)
            };

            foreach (var generator in listOfGenerators)
            {
                generator.Generate();
                db.SaveChanges();
            }

            db.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}
