namespace TheCompany.RandomDataGenerator
{
    using System;
    using System.Linq;
    using TheCompany.Data;

    public class EmployeeDataGenerator : DataGenerator, IDataGenerator
    {
        public EmployeeDataGenerator(IRandomGenerator randomGenerator, TheCompanyEntities theCompanyEntities, int countOfGeneratedObjects)
            :base(randomGenerator, theCompanyEntities, countOfGeneratedObjects)
        {
        }

        public override void Generate()
        {
            var departmentIds = this.Database.Departments.Select(d => d.DepartmentId).ToList();

            Console.WriteLine("Employees adding started");
            for (int i = 0; i < this.CountOfGeneratedObjects; i++)
            {
                var departmentIndex = this.RandomGenerator.GetRandomNumber(0, departmentIds.Count);
                if (departmentIndex == departmentIds.Count)
                {
                    departmentIndex--;
                }
                var departmentId = departmentIds[departmentIndex];

                var employee = new Employee
                {
                    FirstName = this.RandomGenerator.GetRandomString(5, 19),
                    LastName = this.RandomGenerator.GetRandomString(5, 19),
                    YearSalary = this.RandomGenerator.GetRandomNumber(50000, 200000),
                    DepartmentId = departmentId
                };

                if (i == 0 || i == 1)
                {
                    employee.ManagerId = null;
                }
                else // this way the cycles in the management tree are avoided
                {
                    employee.ManagerId = i - 1;
                }
                
                if (i % 100 > 95) // 5 % of the employees won't have managers
                {
                    employee.ManagerId = null;
                }

                if (i % 100 == 0)
                {
                    Console.Write("-");
                    Database.SaveChanges();
                }

                this.Database.Employees.Add(employee);
            }

            Console.WriteLine("\nEmployees adding finished");
        }
    }
}