using lambda_practice.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                 .UseInMemoryDatabase(databaseName: "Sample_Data")
                 .Options;

            // Insert test data in the DB
            using (var context = new DatabaseContext(options))
            {
                var intiliazer = new DatabaseInitializer();
                intiliazer.Initialize(context);
            }

            using (var context = new DatabaseContext(options))
            {
                //NOTA: de preferencia probar de uno por uno.
                
                //1. List all employees whose departament has an office in Chihuahua 
                /*var chih_deps = context.Departments.Where(d => d.Cities.Contains(context.Cities.Single(c => c.Name == "Chihuahua"))).ToList();
                chih_deps.ForEach(d => context.Employees.ToList().ForEach(e => {
                    if (e.DepartmentId == d.Id)
                    {
                        Console.WriteLine(e.FirstName + " " + e.LastName); 
                    }
                        
                }));
                Console.WriteLine("======================================================================== 1");
                */

                //2. List all departaments and the number of employees that belong to each department.
                /*var groups1 = context.Employees.GroupBy(p => p.Department.Name);
                groups1.ToList().ForEach(g => Console.WriteLine(g.Key + ": "+g.Count()));
                Console.WriteLine("======================================================================== 2");
                */

                //3. List all remote employees. That is all employees whose living city is not the same one as their department's.
                /*var employees = context.Employees.Include(e => e.City).Include(e => e.Department).Where(e => !e.Department.Cities.Contains(context.Cities.Single(c => c.Id==e.CityId))).ToList();
                employees.ForEach(e => Console.WriteLine(e.FirstName + " " + e.LastName));
                Console.WriteLine("======================================================================== 3");
                */

                //4. List all employees whose hire aniversary is next month.
                /*int next_month = (Int32.Parse(DateTime.Now.ToString("MM"))+1);
                next_month = next_month >=13? 1 : next_month;
                context.Employees.ToList().ForEach(e => {
                    if (next_month.Equals(e.HireDate.Month))
                    {
                        Console.WriteLine(e.FirstName + " " + e.LastName);
                    }
                });
                Console.WriteLine("======================================================================== 4");
                */

                //5. List all 12 months of the year and the number of employees hired on each month.
                /*var groups2 = context.Employees.GroupBy(p => p.HireDate.Month);
                groups2.ToList().ForEach(g => Console.WriteLine("On " + CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat.GetMonthName((int)g.Key) + ": " + g.Count() + " employees were hired"));
                Console.WriteLine("======================================================================== 5");
                */
            }

            Console.ReadLine();
        }
    }
}
