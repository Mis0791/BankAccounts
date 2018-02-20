using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounts.Models
{
    public class RegisterUser : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
 
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public List<Account> Account { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Balance { get; set; }

        public RegisterUser() 
        {
            Account = new List<Account>();
        }
    }
}