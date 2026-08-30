using System.ComponentModel;

namespace Customer.Domain.Enums;

public enum EmploymentTypes
{
            
    [Description("Payroll")] Payroll,
    [Description("Independent")] Independent,
    [Description("Unemployed")] Unemployed,
}

