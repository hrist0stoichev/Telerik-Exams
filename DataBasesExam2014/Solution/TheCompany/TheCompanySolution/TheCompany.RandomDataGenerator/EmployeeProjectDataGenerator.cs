namespace TheCompany.RandomDataGenerator
{
    using System;
    using System.Linq;

    using TheCompany.Data;

    public class EmployeeProjectDataGenerator : DataGenerator, IDataGenerator
    {
        public EmployeeProjectDataGenerator(IRandomGenerator randomGenerator, TheCompanyEntities theCompanyEntities, int countOfGeneratedObjects)
            : base(randomGenerator, theCompanyEntities, countOfGeneratedObjects)
        {
        }

        public override void Generate()
        {
            var employeeIds = this.Database.Employees.Select(e => e.EmployeeId).ToList();
            var projectIds = this.Database.Projects.Select(p => p.ProjectId).ToList();

            Console.WriteLine("EmployeeProject adding started");
            for (int i = 0; i < projectIds.Count; i++)
            {
                var startingDate = this.RandomGenerator.GetRandomDateTime(new DateTime(1995, 1, 1, 10, 0, 0), new DateTime(2000, 1, 1, 10, 0, 0));
                var endingDate = this.RandomGenerator.GetRandomDateTime(new DateTime(2001, 1, 1, 10, 0, 0), new DateTime(2020, 1, 1, 10, 0, 0));

                for (int j = 0; j < ((i % 20 > 2) ? i % 20 : i % 20 + 2); j++)
                {
                    var employeeProject = new EmployeesProject
                    {
                        EmployeeId = employeeIds[this.RandomGenerator.GetRandomNumber(0, employeeIds.Count)],
                        ProjectId = projectIds[i],
                        StartingDate = startingDate,
                        EndingDate = endingDate
                    };

                    this.Database.EmployeesProjects.Add(employeeProject);
                }

                if (i % 100 == 0)
                {
                    Console.Write("-");
                    this.Database.SaveChanges();
                }
            }

            Console.WriteLine("\nEmloyeeProject adding finished");
        }
    }
}
