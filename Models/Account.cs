using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounts.Models
{
    public class Account : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Display(Name = "Deposit/Withdraw")]
        public int Transaction { get; set; }
        public int UserId { get; set; }

    }
}