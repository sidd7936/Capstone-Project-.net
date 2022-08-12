﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignUpAPI.Models
{
    public class Signup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public double EmployeeInvestment { get; set; }
        public double EmployerInvestment { get; set; }

        public double RetirementInvestment { get; set; }
    }

}
