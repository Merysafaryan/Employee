using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Bogus;
using Employee.Management.system.Responses;

namespace Employee.Management.system.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<Responses.Employee> Employees { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Responses.Employee>().HasData(GetEmployee());
        }

        private List<Responses.Employee> GetEmployee()
        {
            var employees = new List<Responses.Employee>();
            var faker = new Faker("en");

            for (int i = 0; i < 50; i++)
            {
                var employee = new Responses.Employee
                {
                    Id = i,
                    ImgUrl = faker.Internet.Avatar(),
                    Name = faker.Name.FullName(),
                    Salary = GetRandomSalary(),
                    Type = GetRandomEmployeeType(),
                    Position = GetRandomPosition(),
                };
                employees.Add(employee);
            }
            return employees;
        }

        private Position GetRandomPosition()
        {
            var random = new Random();
            var positions = Enum.GetValues(typeof(Position));
            return (Position)positions.GetValue(random.Next(positions.Length));
        }

        private EmployeeType GetRandomEmployeeType()
        {
            var random = new Random();
            var types = Enum.GetValues(typeof(EmployeeType));
            return (EmployeeType)types.GetValue(random.Next(types.Length));
        }

        private decimal GetRandomSalary()
        {
            var random = new Random();
            decimal salary = random.Next(300000, 100000);
            return salary;
        }
    }
}
