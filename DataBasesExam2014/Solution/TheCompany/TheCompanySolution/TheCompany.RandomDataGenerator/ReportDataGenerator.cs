namespace TheCompany.RandomDataGenerator
{
    using System;
    using System.Linq;

    using TheCompany.Data;

    public class ReportDataGenerator : DataGenerator, IDataGenerator
    {
        public ReportDataGenerator(IRandomGenerator randomGenerator, TheCompanyEntities theCompanyEntities, int countOfGeneratedObjects)
            :base(randomGenerator, theCompanyEntities, countOfGeneratedObjects)
        {
        }
        
        public override void Generate()
        {
            var employeeIds = this.Database.Employees.Select(e => e.EmployeeId).ToList();
            Console.WriteLine("Reports adding started");
            for (int i = 0; i < this.CountOfGeneratedObjects; i++)
			{
                var report = new Report
                {
                    TimeOfReport = this.RandomGenerator.GetRandomDateTime(new DateTime(1995, 1, 1, 10, 0, 0), new DateTime(2020, 1, 1, 10, 0, 0)),
                    EmployeeId = employeeIds[i % 50]
                };

                if (i % 100 == 0)
                {
                    Console.Write("-");
                    Database.SaveChanges();
                }

                this.Database.Reports.Add(report);
			}

            Console.WriteLine("\nReports adding finished");
        }
    }
}
