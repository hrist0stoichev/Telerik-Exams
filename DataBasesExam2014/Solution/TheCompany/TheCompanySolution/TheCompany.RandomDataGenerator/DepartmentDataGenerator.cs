namespace TheCompany.RandomDataGenerator
{
    using System;
    using System.Collections.Generic;

    using TheCompany.Data;

    public class DepartmentDataGenerator : DataGenerator, IDataGenerator
    {
        public DepartmentDataGenerator(IRandomGenerator randomGenerator, TheCompanyEntities theCompanyEntities, int countOfGeneratedObjects)
            :base(randomGenerator, theCompanyEntities, countOfGeneratedObjects)
        {
        }
         
        public override void Generate()
        {
            var departmentsToBeAdded = new HashSet<string>();
            while (departmentsToBeAdded.Count != this.CountOfGeneratedObjects)
            {
                departmentsToBeAdded.Add(this.RandomGenerator.GetRandomString(10, 50));
            }

            int index = 0;
            Console.WriteLine("Departments adding started");

            foreach (var departmentName in departmentsToBeAdded)
            {
                var department = new Department
                {
                    Name = departmentName
                };

                if (index % 100 == 0)
                {
                    Console.Write("-");
                    Database.SaveChanges();
                }

                this.Database.Departments.Add(department);
                index++;
            }

            Console.WriteLine("\nDepartments adding finished");
        }
    }
}
