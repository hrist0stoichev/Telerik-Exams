namespace TheCompany.RandomDataGenerator
{
    using System;
    using System.Linq;

    using TheCompany.Data;

    public class ProjectDataGenerator : DataGenerator, IDataGenerator
    {
        public ProjectDataGenerator(IRandomGenerator randomGenerator, TheCompanyEntities theCompanyEntities, int countOfGeneratedObjects)
            :base(randomGenerator, theCompanyEntities, countOfGeneratedObjects)
        {
        }

        public override void Generate()
        {
            var employeeIds = this.Database.Employees.Select(e => e.EmployeeId).ToList();
            
            Console.WriteLine("Projects adding started");
            for (int i = 0; i < this.CountOfGeneratedObjects; i++)
            {
                var project = new Project
                {
                    Name = this.RandomGenerator.GetRandomString(5, 50)
                };

                if (i % 100 == 0)
                {
                    Console.Write("-");
                    this.Database.SaveChanges();
                }

                this.Database.Projects.Add(project);
            }

            Console.WriteLine("\nProject adding finished");

        }
    }
}
