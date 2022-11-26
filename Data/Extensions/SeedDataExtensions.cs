using Common.Extensions;
using Domain.Entities;
using System;
using System.Linq;

namespace Data.Extensions
{
    public static class SeedDataExtensions
    {
        public static void EnsureSeeded(this DatabaseContext context)
        {
            SeedUser(context);
        }

        private static void SeedUser(DatabaseContext context)
        {
            if (context.User.Any())
                return;

            var (hashedPassword, passwordKey) = "123456".ToPasswordHmacSha512Hash();

            context.User.Add(new User
            {
                FirstName = "Code",
                LastName = "demo",
                EmailAddress = "example@gmail.com",
                Password = hashedPassword,
                PasswordKey = passwordKey,
                MobileNumber = "015218413607",
                CreationTs = DateTime.UtcNow
            });

            context.SaveChanges();
        }

        private static void SeedCustomer(DatabaseContext context)
        {
            if (context.Customer.Any())
                return;

            
            context.Customer.Add(new Customer
            {
                FirstName = "Aaaa",
                LastName = "Aaaa",
                Age = 20,
                CreationTs = DateTime.UtcNow
            });

            context.Customer.Add(new Customer
            {
                FirstName = "Bbbb",
                LastName = "Aaaa",
                Age = 56,
                CreationTs = DateTime.UtcNow
            });

            context.Customer.Add(new Customer
            {
                FirstName = "Bbbb",
                LastName = "Bbbb",
                Age = 26,
                CreationTs = DateTime.UtcNow
            });

            context.Customer.Add(new Customer
            {
                FirstName = "Aaaa",
                LastName = "Cccc",
                Age = 32,
                CreationTs = DateTime.UtcNow
            });

            context.Customer.Add(new Customer
            {
                FirstName = "Bbbb",
                LastName = "Cccc",
                Age = 50,
                CreationTs = DateTime.UtcNow
            });

            context.Customer.Add(new Customer
            {
                FirstName = "Aaaa",
                LastName = "Dddd",
                Age = 70,
                CreationTs = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }
}
