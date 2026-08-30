using System;
using System.Collections.Generic;
using System.Text;
using Customer.Domain.Enums;

namespace Customer.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public decimal MonthlyIncome { get; set; }
        public EmploymentTypes EmploymentType { get; set; }
        public DateTime CreatedUtc { get; set; }

        public static Customer Create(string fullName, string documentNumber, string email, decimal monthlyIncome, EmploymentTypes employmentType)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(documentNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);


            if (!Enum.IsDefined(typeof(EmploymentTypes), employmentType))
                throw new ArgumentException("The employment type is not valid", nameof(employmentType));
            
            if (monthlyIncome <= 0)
                throw new ArgumentOutOfRangeException((nameof(monthlyIncome)), "Monthly income must be greater than 0");


            return new Customer
            {
                Id = Guid.NewGuid(),
                FullName = fullName.Trim(),
                DocumentNumber = documentNumber.Trim(),
                Email = email.Trim(),
                MonthlyIncome = monthlyIncome,
                EmploymentType = employmentType,
                CreatedUtc = DateTime.UtcNow
            };
        }

    }

}
